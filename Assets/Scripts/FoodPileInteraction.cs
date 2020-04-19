using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player's interaction with the food pile.
/// This is more of a first pass at interactions in general; this may
/// be refactored or replaced once they are expanded on.
/// </summary>
public class FoodPileInteraction : MonoBehaviour
{
    public FoodPieceScript foodPrefab;
    public PetStatus petStatus;
    public Camera sceneCamera;

    public float healthRestoreAmount = 10;
    public float needinessIncreaseAmount = 10;

    // function that gets called when you click on this
    public void attachFoodToCursor()
    {
        GameObject new_food = Instantiate(foodPrefab.gameObject, transform);
        FoodPieceScript new_food_script = new_food.GetComponent<FoodPieceScript>();
        new_food_script.Initialize(healthRestoreAmount, needinessIncreaseAmount,
            petStatus);
    }
}
