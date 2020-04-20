using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains data related to the pet's condition, and provides ways
/// to access or update it.
/// </summary>
public class PetStatus : MonoBehaviour
{
    [SerializeField]
    public Material neutralSprite;
    [SerializeField]
    public Material happySprite;
    [SerializeField]
    public Material hungrySprite;
    [SerializeField]
    public Material angrySprite;

    // Stats/conditions
    [SerializeField]
    private PetCondition current_health;
    [SerializeField]
    private PetCondition current_neediness;

    // Rates of change
    [SerializeField]
    private float default_hunger_rate = -2;
    [SerializeField]
    private float default_neediness_decay = -0.5f;

    private float hunger_threshold = 33;
    private float anger_threshold = 50;
    private float happy_threshold = 80;

    [SerializeField]
    private List<PetCondition> current_conditions;
    private Dictionary<string, int> condition_indices;

    private Renderer rend;
    private bool isInanimate;

    //Constants
    private static float MAX_HEALTH = 100;
    private static float OVERFEED_CONVERSION = 1.5f;

    [Serializable]
    /// <summary>
    /// Encapuslates several values that might be relevant to a stat/condition.
    /// </summary>
    private class PetCondition
    {
        [SerializeField]
        private float condition_value;
        // A function to see how much to update the condition_value by
        private Func<float> value_delta;

        // Bounds on the value; a number less than zero indicates no bound
        private float minimum_value = -1;
        private float maximum_value = -1;

        /// <summary>
        /// Creates a representation of a new condition for the pet.
        /// </summary>
        /// <param name="initial_value">
        ///     The initial value of the condition. </param>
        /// <param name="value_delta">
        ///     A function which, when called, returns how much the condition value
        ///     should be changed by relative to the last Update call. </param>
        public PetCondition(float initial_value, Func<float> value_delta)
        {
            this.condition_value = initial_value;
            this.value_delta = value_delta;
        }

        /// <summary>
        /// Creates a representation of a new condition for the pet.
        /// </summary>
        /// <param name="initial_value">
        ///     The initial value of the condition. </param>
        /// <param name="value_increment">
        ///     The amount by which the condition value should be changed in
        ///     one second. </param>
        public PetCondition(float initial_value, float value_increment)
        {
            this.condition_value = initial_value;
            this.value_delta = () => value_increment * Time.deltaTime;
        }


        /// <summary>
        /// Updates the condition value using the value_delta function
        /// </summary>
        public void on_update()
        {
            this.condition_value += this.value_delta();
            this.bindValue();
        }

        // Getters and setters

        public float getValue()
        {
            return this.condition_value;
        }

        public void setValue(float new_value)
        {
            this.condition_value = new_value;
            this.bindValue();
        }

        public void incrementValue(float value_increment)
        {
            this.condition_value += value_increment;
            this.bindValue();
        }

        public void setValueIncrement(float new_increment)
        {
            this.value_delta = () => new_increment * Time.deltaTime;
        }

        public void setMinimum(float min_value)
        {
            this.minimum_value = min_value;
            this.bindValue();
        }

        public void setMaximum(float max_value)
        {
            this.maximum_value = max_value;
            this.bindValue();
        }

        public void setBounds(float min_value, float max_value)
        {
            setMinimum(min_value);
            setMaximum(max_value);
        }

        private void bindValue()
        {
            if (this.minimum_value >= 0 && this.condition_value < this.minimum_value)
            {
                this.condition_value = this.minimum_value;
            }
            else if (this.maximum_value >= 0 && this.condition_value > this.maximum_value)
            {
                this.condition_value = this.maximum_value;
            }
        }
    }

    /* Uncomment this if you want to be able to add conditions in the editor
    [ContextMenuItem("Add Condition", "debugAddCondition")]
    public string newDebugConditionName;
    public void debugAddCondition()
    {
        addNewCondition(newDebugConditionName, -1);
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        current_health = new PetCondition(MAX_HEALTH, getHungerDelta);
        current_health.setBounds(0, MAX_HEALTH);
        current_neediness = new PetCondition(0, default_neediness_decay);
        current_neediness.setMinimum(0);

        current_conditions = new List<PetCondition>();
        condition_indices = new Dictionary<string, int>();

        rend = GetComponentInChildren<Renderer>();
        rend.material = neutralSprite;
        isInanimate = false;
    }

    private float getHungerDelta()
    {
        return isInanimate ? 0 : default_hunger_rate * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        current_health.on_update();
        current_neediness.on_update();

        foreach (PetCondition condition in current_conditions)
        {
            condition.on_update();
        }

        // updating sprite
        if (getHealth() < hunger_threshold)
        {
            updateSprite(hungrySprite);
        }
        else
        {
            float happiness = getHappiness();
            if (happiness < anger_threshold)
            {
                updateSprite(angrySprite);
            }
            else if (happiness < happy_threshold)
            {
                updateSprite(neutralSprite);
            } else
            {
                updateSprite(happySprite);
            }
        }
    }

    private void updateSprite(Material newSprite)
    {
        if (rend.material != newSprite)
        {
            rend.material = newSprite;
        }
    }

    /// <summary>
    /// Creates a new condition for the pet. It will have a range of 0 to 100, and it
    /// will start out at 100.
    /// </summary>
    /// <param name="condition_name">The name for the condition.</param>
    /// <param name="value_delta">A function that returns the amount by which the
    /// value should change between calls to Update, generally used for the
    /// condition's natural decay.</param>
    public void addNewCondition(string condition_name, Func<float> value_delta)
    {
        if (condition_indices.ContainsKey(condition_name))
        {
            Debug.LogWarning("Warning: conditions with repeated names created");
        }

        PetCondition new_condition = new PetCondition(100, value_delta);
        new_condition.setBounds(0, 100);

        int new_index = current_conditions.Count;
        current_conditions.Add(new_condition);
        condition_indices.Add(condition_name, new_index);
    }

    /// <summary>
    /// Creates a new condition for the pet. It will have a range of 0 to 100, and it
    /// will start out at 100.
    /// </summary>
    /// <param name="condition_name">The name for the condition.</param>
    /// <param name="value_increment">The amount by which the condition's value changes every
    /// second by default.</param>
    public void addNewCondition(string condition_name, float value_increment)
    {
        addNewCondition(condition_name, () => value_increment * Time.deltaTime);
    }

    // Helper function to look up the condition object by name
    private PetCondition getConditionFromString(string condition_name)
    {
        if (!condition_indices.ContainsKey(condition_name))
        {
            return null;
        }

        int condition_index = condition_indices[condition_name];
        return current_conditions[condition_index];
    }

    public void updatePetFormStats(float new_needinessDelta, Material new_defaultSprite,
        Material new_happySprite, Material new_hungrySprite, Material new_angrySprite,
        bool inanimate)
    {
        current_neediness.setValueIncrement(new_needinessDelta);
        neutralSprite = new_defaultSprite;
        happySprite = new_happySprite;
        hungrySprite = new_hungrySprite;
        angrySprite = new_angrySprite;

        isInanimate = inanimate;
        if (isInanimate)
        {
            current_health.setValue(MAX_HEALTH);
        }
    }


    // Getters and setters
    public float getHealth()
    {
        return this.current_health.getValue();
    }
    public void increaseHealth(float additional_health)
    {
        float overfeed = (getHealth() + additional_health) - MAX_HEALTH;
        if (overfeed > 0)
        {
            increaseNeediness(overfeed * OVERFEED_CONVERSION);
        }

        this.current_health.incrementValue(additional_health);
    }

    public float getNeediness()
    {
        return this.current_neediness.getValue();
    }
    public void increaseNeediness(float additional_neediness)
    {
        this.current_neediness.incrementValue(additional_neediness);
    }

    public float getConditionValue(string condition_name)
    {
        PetCondition condition = getConditionFromString(condition_name);
        if (condition != null)
        {
            return condition.getValue();
        } else
        {
            throw new KeyNotFoundException("Undefined condition name " + condition_name);
        }
    }
    public void increaseConditionValue(string condition_name, float additional_value)
    {
        PetCondition condition = getConditionFromString(condition_name);
        if (condition != null)
        {
            condition.incrementValue(additional_value);
        }
        else
        {
            throw new KeyNotFoundException("Undefined condition name " + condition_name);
        }
    }

    public Dictionary<string, float> getAllConditionValues()
    {
        Dictionary<string, float> result = new Dictionary<string, float>();
        foreach (string condition_name in condition_indices.Keys)
        {
            result.Add(condition_name, getConditionValue(condition_name));
        }
        return result;
    }

    // Likely inefficient
    public float getHappiness()
    {
        float min_happiness = 100;
        foreach (PetCondition condition in current_conditions)
        {
            float value = condition.getValue();
            if (value == 0)
            {
                return 0;
            } else if (value < min_happiness)
            {
                min_happiness = value;
            }
        }

        return min_happiness;
    }

    public bool isSatisfied()
    {
        return getHappiness() >= anger_threshold;
    }
}