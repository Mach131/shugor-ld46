using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetsInteractionData : MonoBehaviour, InteractionData
{
    public SweetsPileInteraction sweetsPile;

    [SerializeField]
    private string conditionName = "Sweet Tooth";
    [SerializeField]
    private float valueDecay = -2.5f;
    [SerializeField]
    private float conditionIncreaseAmount = 10;
    [SerializeField]
    private float needinessIncreaseAmount = 25;

    public void addCondition(PetStatus petStatus)
    {
        petStatus.addNewCondition(conditionName, valueDecay);
    }

    public void instantiateInteractions(PetStatus petStatus)
    {
        SweetsPileInteraction newSweestPile = Instantiate(sweetsPile.gameObject,
            transform).GetComponent<SweetsPileInteraction>();
        newSweestPile.Initialize(petStatus, conditionName,
            conditionIncreaseAmount, needinessIncreaseAmount);
    }
}
