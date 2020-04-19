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
    // Stats/conditions
    [SerializeField]
    private PetCondition current_health;
    [SerializeField]
    private PetCondition current_neediness;

    // TODO: Placeholder condition; will make this more scalable later
    [SerializeField]
    private PetCondition current_entertainment;


    // Rates of change
    [SerializeField]
    private float default_hunger_rate = -2;
    [SerializeField]
    private float default_neediness_decay = -0.5f;

    //Constants
    private static float MAX_HEALTH = 100;

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
            if (this.minimum_value >= 0 && this.condition_value < this.minimum_value)
            {
                this.condition_value = this.minimum_value;
            }
            else if (this.maximum_value >= 0 && this.condition_value > this.maximum_value)
            {
                this.condition_value = this.maximum_value;
            }
        }

        // Getters and setters

        public float getValue()
        {
            return this.condition_value;
        }

        public void setValue(float new_value)
        {
            this.condition_value = new_value;
        }

        public void set_minimum(float min_value)
        {
            this.minimum_value = min_value;
        }

        public void set_maximum(float max_value)
        {
            this.maximum_value = max_value;
        }

        public void set_bounds(float min_value, float max_value)
        {
            set_minimum(min_value);
            set_maximum(max_value);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        current_health = new PetCondition(MAX_HEALTH, default_hunger_rate);
        current_health.set_bounds(0, MAX_HEALTH);
        current_neediness = new PetCondition(0, default_neediness_decay);
        current_neediness.set_minimum(0);

        // TODO: placeholder
        current_entertainment = new PetCondition(100, -1);
        current_entertainment.set_bounds(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        current_health.on_update();
        current_neediness.on_update();

        // TODO: loop through secondary conditions
        current_entertainment.on_update();
    }


    // Getters and setters

    public float getHealth()
    {
        return this.current_health.getValue();
    }
    public void increaseHealth(float bonusHealth)
    {
        this.current_health.setValue(getHealth() + bonusHealth);

    }

    public float getNeediness()
    {
        return this.current_neediness.getValue();
    }
    public void increaseNeediness(float bonusNeediness)
    {
        this.current_neediness.setValue(getNeediness() + bonusNeediness);
    }

    // TODO: Replace with more general things later
    public float getEntertainment()
    {
        return this.current_entertainment.getValue();
    }
    public void increaseEntertainment(float bonusEntertainment)
    {
        this.current_entertainment.setValue(getEntertainment() + bonusEntertainment);
    }
}