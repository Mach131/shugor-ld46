using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player's interactions with a food piece.
/// May be adjusted as interactions are generalized.
/// </summary>
public class FoodPieceScript : MonoBehaviour
{
    private float healthRestoreAmount = 10;
    private float needinessIncreaseAmount = 10;
    private PetStatus petStatus;
    private Collider petColl;

    private Collider coll;

    public void Initialize(float healthRestoreAmount, float needinessIncreaseAmount,
        PetStatus petStatus)
    {
        this.healthRestoreAmount = healthRestoreAmount;
        this.needinessIncreaseAmount = needinessIncreaseAmount;
        this.petStatus = petStatus;

        this.petColl = petStatus.GetComponentInChildren<Collider>();
        this.coll = GetComponentInChildren<Collider>();
    }

    // Called when the mouse button is released
    public void releaseFoodPiece()
    {
        bool checkPetOverlap = Utils.checkOverlap(coll, petColl);
        if (checkPetOverlap)
        {
            this.petStatus.increaseHealth(healthRestoreAmount);
            this.petStatus.increaseNeediness(needinessIncreaseAmount);
            Debug.Log("yum");
        }
        GameObject.Destroy(gameObject);
    }
}
