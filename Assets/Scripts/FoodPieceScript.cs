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
    private Camera sceneCamera;

    private Collider coll;

    private void Update()
    {
        if (sceneCamera != null)
        {
            Vector3 mousePos = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
            float z = this.transform.position.z;
            this.transform.position = new Vector3(mousePos.x, mousePos.y, z);
        }
    }

    public void Initialize(float healthRestoreAmount, float needinessIncreaseAmount,
        PetStatus petStatus, Camera sceneCamera)
    {
        this.healthRestoreAmount = healthRestoreAmount;
        this.needinessIncreaseAmount = needinessIncreaseAmount;
        this.petStatus = petStatus;
        this.sceneCamera = sceneCamera;

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
