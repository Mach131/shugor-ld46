using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseInteractionData : MonoBehaviour, InteractionData
{
    public BouncingBallInteraction bouncingBall;

    [SerializeField]
    private string condition_name = "Entertainment";
    [SerializeField]
    private float value_decay = -5;
    [SerializeField]
    private float value_increase_amount = 20;
    [SerializeField]
    private float neediness_increase_amount = 25;

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
