using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdlessnessInteractionData : MonoBehaviour, InteractionData
{
    public BirdSpawner birdSpawner;

    [SerializeField]
    private string conditionName = "Birdlessness";
    [SerializeField]
    private float baseValueDecay = -1;
    [SerializeField]
    private float maxValueDecay = -10;
    [SerializeField]
    private float decayPerMiss = -2;
    [SerializeField]
    private float valueIncreaseAmount = 20;
    [SerializeField]
    private float needinessIncreaseAmount = 20;

    private int missedBirds;

    private void Start()
    {
        missedBirds = 0;
    }

    public void addCondition(PetStatus petStatus)
    {
        petStatus.addNewCondition(conditionName, getValueDecay);
    }

    public void instantiateInteractions(PetStatus petStatus)
    {
        BirdSpawner instantiatedSpawner = Instantiate(
            birdSpawner.gameObject, transform).GetComponent<BirdSpawner>();
        instantiatedSpawner.Initialize(petStatus, conditionName,
            valueIncreaseAmount, needinessIncreaseAmount, this);
    }

    private float getValueDecay()
    {
        float birdBasedDecay = baseValueDecay + (missedBirds * decayPerMiss);
        return Mathf.Max(birdBasedDecay, maxValueDecay) * Time.deltaTime;
    }

    public void birdMissed()
    {
        missedBirds += 1;
    }
    public void birdHit()
    {
        missedBirds = 0;
    }
}
