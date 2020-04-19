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
    public Object foodPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // function that gets called when you click on this
    public void attachFoodToCursor()
    {
        Debug.Log("clicc");
    }
}
