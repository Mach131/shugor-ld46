using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanityInteractionController : MonoBehaviour, InteractionData
{
    public BrushInteraction brush;

    [SerializeField]
    private string conditionName = "Vanity";
    [SerializeField]
    private float valueDecay = -4;
    [SerializeField]
    private float valueIncreasePerSecond = 20;
    [SerializeField]
    private float needinessIncreasePerSecond = 30;

    public void addCondition(PetStatus petStatus)
    {
        petStatus.addNewCondition(conditionName, valueDecay);
    }

    public void instantiateInteractions(PetStatus petStatus)
    {
        BrushInteraction instantiatedBrush = Instantiate(brush.gameObject, transform)
            .GetComponent<BrushInteraction>();
        instantiatedBrush.Initialize(petStatus, conditionName,
            valueIncreasePerSecond, needinessIncreasePerSecond);
    }
}
