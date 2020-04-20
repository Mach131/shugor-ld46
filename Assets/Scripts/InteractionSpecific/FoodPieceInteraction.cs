using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player's interactions with a food piece.
/// </summary>
public class FoodPieceInteraction : MonoBehaviour
{
    private float healthRestoreAmount = 15;
    private float needinessIncreaseAmount = 10;
    public Transform FeedParticles;
    private PetStatus petStatus;
    private Collider petColl;

    private Collider coll;
    private FallingObject faller;
    private bool falling;

    private void Start()
    {
        this.coll = GetComponentInChildren<Collider>();
        this.faller = GetComponent<FallingObject>();
        falling = false;
    }

    private void Update()
    {
        if (falling)
        {
            bool checkPetOverlap = Utils.checkOverlap(coll, petColl);
            if (checkPetOverlap)
            {
                this.petStatus.increaseHealth(healthRestoreAmount);
                this.petStatus.increaseNeediness(needinessIncreaseAmount);
                //Debug.Log("yum");
                Instantiate(FeedParticles, transform.position + new Vector3(0.0f, 0.0f, -1f), Quaternion.Euler(0f, 0f, 0f));
                SoundEffectHandler.player.playRandomNeutralSound();
                GameObject.Destroy(gameObject);
            }
        }
    }

    public void Initialize(float healthRestoreAmount, float needinessIncreaseAmount,
        PetStatus petStatus)
    {
        this.healthRestoreAmount = healthRestoreAmount;
        this.needinessIncreaseAmount = needinessIncreaseAmount;
        this.petStatus = petStatus;

        this.petColl = petStatus.GetComponentInChildren<Collider>();
    }

    // Called when the mouse button is released
    public void releaseFoodPiece()
    {
        falling = true;
        faller.enabled = true;
    }
}
