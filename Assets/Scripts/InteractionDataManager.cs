using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to help manage the creation and messaging of all interaction systems.
/// Having them perform their initialization functions through this class allows
/// it to keep references to all of the active interaction systems, which is useful
/// if some operation needs to be performed on all of them at once.
/// </summary>
public class InteractionDataManager : MonoBehaviour
{
    public PetStatus petStatus;

    [SerializeField]
    private List<InteractionData> activeInteractionSystems = new List<InteractionData>();

    ///* Uncomment this for testing
    [ContextMenuItem("Start interaction system", "testInteraction")]
    public GameObject testInteractionData;
    public void testInteraction()
    {
        startInteractionSystem(testInteractionData.GetComponent<InteractionData>());
    }
    //*/

    public void startInteractionSystem(InteractionData interactionData)
    {
        interactionData.addCondition(petStatus);
        interactionData.instantiateInteractions(petStatus);

        activeInteractionSystems.Add(interactionData);
    }
}
