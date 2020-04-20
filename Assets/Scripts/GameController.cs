using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the general flow of the game.
/// </summary>
public class GameController : MonoBehaviour
{
    public PetFormController petFormController;

    [SerializeField]
    private float TOTAL_GAME_DURATION = 60 * 10;
    [SerializeField]
    private float FIRST_TRANSFORM_TIME = 60 * 3;
    [SerializeField]
    private float SECOND_TRANSFORM_TIME = 60 * 6;

    private float currentTime;
    private int transformationIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        transformationIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if ((transformationIndex == 0 && currentTime >= FIRST_TRANSFORM_TIME) ||
            (transformationIndex == 1 && currentTime >= SECOND_TRANSFORM_TIME))
        {
            petFormController.advancePetForm();
            transformationIndex += 1;
        }
    }

    public float getTotalDuration()
    {
        return TOTAL_GAME_DURATION;
    }
    public float getRemainingTIme()
    {
        return TOTAL_GAME_DURATION - currentTime;
    }
}
