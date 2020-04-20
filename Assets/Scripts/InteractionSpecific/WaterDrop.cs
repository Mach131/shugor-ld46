using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behavior of the water drop.
/// </summary>
public class WaterDrop : MonoBehaviour
{
    private PetStatus petStatus;
    private string conditionName;
    private float conditionRestoreAmount;
    private float needinessIncreaseAmount;

    private Collider petColl;
    private Collider coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponentInChildren<Collider>();
    }

    public void Initialize(PetStatus petStatus, string conditionName,
        float conditionRestoreAmount, float needinessIncreaseAmount)
    {
        this.petStatus = petStatus;
        this.conditionName = conditionName;
        this.conditionRestoreAmount = conditionRestoreAmount;
        this.needinessIncreaseAmount = needinessIncreaseAmount;

        petColl = petStatus.GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Utils.checkOverlap(coll, petColl))
        {
            petStatus.increaseConditionValue(conditionName, conditionRestoreAmount);
            petStatus.increaseNeediness(needinessIncreaseAmount);
            SoundEffectHandler.player.playRandomNeutralSound();
            //Debug.Log("wosh");
            GameObject.Destroy(gameObject);
        }
    }
}
