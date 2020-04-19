using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player's interaction with the food pile.
/// </summary>
public class FoodPileInteraction : MonoBehaviour
{
    public FoodPieceInteraction foodPrefab;
    public PetStatus petStatus;

    public float healthRestoreAmount = 10;
    public float needinessIncreaseAmount = 10;

    // function that gets called when you click on this
    public void attachFoodToCursor()
    {
        GameObject new_food = Instantiate(foodPrefab.gameObject, transform);
        FoodPieceInteraction new_food_script = new_food.GetComponent<FoodPieceInteraction>();
        new_food_script.Initialize(healthRestoreAmount, needinessIncreaseAmount,
            petStatus);
    }
}
