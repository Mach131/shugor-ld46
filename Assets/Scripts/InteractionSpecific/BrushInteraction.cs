using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls interactions with the brush.
/// </summary>
public class BrushInteraction : MonoBehaviour
{
    public Transform BrushParticle;

    private PetStatus petStatus;
    private string conditionName;
    private float conditionRestorePerSecond;
    private float needinessIncreasePerSecond;

    private Collider petColl;
    private Collider coll;
    private FallingObject faller;

    private bool currentlyHeld;
    private Transform currentParticle;

    private void Start()
    {
        this.coll = GetComponentInChildren<Collider>();
        faller = GetComponent<FallingObject>();
        currentlyHeld = false;
    }

    public void Initialize(PetStatus petStatus, string conditionName,
        float conditionRestorePerSecond, float needinessIncreasePerSecond)
    {
        this.petStatus = petStatus;
        this.conditionName = conditionName;
        this.conditionRestorePerSecond = conditionRestorePerSecond;
        this.needinessIncreasePerSecond = needinessIncreasePerSecond;

        this.petColl = petStatus.GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyHeld && Utils.checkOverlap(coll, petColl))
        {
            float conditionIncrease = conditionRestorePerSecond * Time.deltaTime;
            float needinessIncrease = needinessIncreasePerSecond * Time.deltaTime;

            petStatus.increaseConditionValue(conditionName, conditionIncrease);
            petStatus.increaseNeediness(needinessIncrease);

            if (currentParticle == null)
            {
                currentParticle = Instantiate(BrushParticle,
                    transform.position + new Vector3(0.5f, 0.5f, -1f),
                    Quaternion.Euler(0f, 0f, 0f));
                SoundEffectHandler.player.playRandomNeutralSound();
            }
        }
    }

    // Called when drag begins
    public void pickedUp()
    {
        currentlyHeld = true;
        faller.enabled = false;
    }

    // Called when drag ends
    public void dropped()
    {
        currentlyHeld = false;
        faller.enabled = true;
    }
}
