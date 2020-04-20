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
    private PetStatus petStatus;
    private EndingManager endingManager;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        transformationIndex = 0;

        endingManager = GetComponent<EndingManager>();
        petStatus = petFormController.GetComponent<PetStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (petStatus.getHealth() <= 0)
        {
            endingManager.toHungryEnding();
        } else
        {
            currentTime += Time.deltaTime;

            if ((transformationIndex == 0 && currentTime >= FIRST_TRANSFORM_TIME) ||
                (transformationIndex == 1 && currentTime >= SECOND_TRANSFORM_TIME))
            {
                petFormController.advancePetForm();
                transformationIndex += 1;
            }

            if (currentTime >= TOTAL_GAME_DURATION)
            {
                endingManager.toStandardEnding();
            }
        }
    }

    public float getTotalDuration()
    {
        return TOTAL_GAME_DURATION;
    }
    public float getRemainingTime()
    {
        return TOTAL_GAME_DURATION - currentTime;
    }
}
