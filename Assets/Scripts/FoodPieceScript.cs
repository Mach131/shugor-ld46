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
    private Camera sceneCamera;

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
    }

    // Called when the mouse button is released
    public void releaseFoodPiece()
    {
        //TODO: check if overlapping the pet, restore hunger if so
        Debug.Log("released");
        GameObject.Destroy(gameObject);
    }

    private void checkPetOverlap()
    {
        Collider petBounds = petStatus.GetComponentInChildren<Collider>();
    }
}
