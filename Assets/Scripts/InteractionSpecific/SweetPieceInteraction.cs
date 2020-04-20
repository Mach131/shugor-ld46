using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player's interaction with a food piece.
/// </summary>
public class SweetPieceInteraction : MonoBehaviour
{
    public Transform SweetParticle;

    private PetStatus petStatus;
    private string conditionName;
    private float conditionRestoreValue;
    private float needinessIncreaseAmount;
    private Collider petColl;

    private Collider coll;
    private FallingObject faller;
    private bool falling;

    // Start is called before the first frame update
    void Start()
    {
        this.coll = GetComponentInChildren<Collider>();
        this.faller = GetComponent<FallingObject>();
        falling = false;
    }

    public void Initialize(PetStatus petStatus, string conditionName,
        float conditionRestoreValue, float needinessIncreaseAmount)
    {
        this.petStatus = petStatus;
        this.conditionName = conditionName;
        this.conditionRestoreValue = conditionRestoreValue;
        this.needinessIncreaseAmount = needinessIncreaseAmount;

        this.petColl = petStatus.GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
        {
            bool checkPetOverlap = Utils.checkOverlap(coll, petColl);
            if (checkPetOverlap)
            {
                this.petStatus.increaseConditionValue(
                    conditionName, conditionRestoreValue);
                this.petStatus.increaseNeediness(needinessIncreaseAmount);
                Instantiate(SweetParticle,
                    transform.position + new Vector3(0.0f, 0.0f, -1f),
                    Quaternion.Euler(0f, 0f, 0f));
                GameObject.Destroy(gameObject);
            }
        }
    }

    // Called when the mouse button is released
    public void releaseSweet()
    {
        falling = true;
        faller.enabled = true;
    }
}
