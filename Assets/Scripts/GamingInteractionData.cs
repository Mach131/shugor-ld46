using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamingInteractionData : MonoBehaviour, InteractionData
{
    public SwitchGameInteraction switchGame;

    [SerializeField]
    private string conditionName = "Gaming";
    [SerializeField]
    private float valueDecay = -33;
    [SerializeField]
    private float valueIncreasePerSecond = 48;
    [SerializeField]
    private float needinessIncreasePerSecond = 30;

    public void addCondition(PetStatus petStatus)
    {
        petStatus.addNewCondition(conditionName, valueDecay);
    }

    public void instantiateInteractions(PetStatus petStatus)
    {
        SwitchGameInteraction instantiatedGame =
            Instantiate(switchGame.gameObject, transform)
            .GetComponent<SwitchGameInteraction>();
        instantiatedGame.Initialize(petStatus, conditionName,
            valueIncreasePerSecond, needinessIncreasePerSecond);
    }
}
