﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanlinessInteractionData : MonoBehaviour, InteractionData
{
    public WaterBucketInteraction bucket;

    private static string condition_name = "Cleanliness";
    private static float value_decay = -4;
    private static float value_increase_amount = 50;
    private static float neediness_increase_amount = 25;

    private WaterBucketInteraction instantiatedBucket;

    public void addCondition(PetStatus petStatus)
    {
        petStatus.addNewCondition(condition_name, getValueDecay);
    }

    public void instantiateInteractions(PetStatus petStatus)
    {
        instantiatedBucket = Instantiate(bucket.gameObject, transform)
            .GetComponent<WaterBucketInteraction>();
        instantiatedBucket.Initialize(petStatus, condition_name,
            value_increase_amount, neediness_increase_amount);
    }

    private float getValueDecay()
    {
        if (instantiatedBucket == null || !instantiatedBucket.isOverfull())
        {
            return value_decay * Time.deltaTime;
        } else
        {
            return value_decay * 2 * Time.deltaTime;
        }
    }
}

