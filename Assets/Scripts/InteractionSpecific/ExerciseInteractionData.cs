using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseInteractionData : MonoBehaviour, InteractionData
{
    public BouncingBallInteraction bouncingBall;

    private static string condition_name = "Entertainment";
    private static float value_decay = -5;
    private static float value_increase_amount = 20;
    private static float neediness_increase_amount = 25;

    public void addCondition(PetStatus petStatus)
    {
        petStatus.addNewCondition(condition_name, value_decay);
    }

    public void instantiateInteractions(PetStatus petStatus)
    {
        GameObject new_ball = GameObject.Instantiate(bouncingBall.gameObject, transform);
        new_ball.GetComponent<BouncingBallInteraction>().Initialize(petStatus, condition_name,
            value_increase_amount, neediness_increase_amount);
    }
}
