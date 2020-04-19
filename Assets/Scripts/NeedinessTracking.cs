using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of the pet's neediness and unlocks new interaction systems
/// as appropriate.
/// </summary>
public class NeedinessTracking : MonoBehaviour
{
    public InteractionDataManager interactionManager;

    // Maps the amount of neediness required for a new interaction to the
    // object containing that interaction's data.
    [Serializable]
    public struct NeedinessReq
    {
        public float needinessRequirement;
        public GameObject unlockedInteraction;
    }
    public List<NeedinessReq> needinessToUnlockedInteractions;

    private PetStatus petStatus;
    private Dictionary<float, InteractionData> interactionUnlockData;
    private List<float> unlockValues;
    private int unlockIndex;

    // Start is called before the first frame update
    void Start()
    {
        petStatus = GetComponent<PetStatus>();

        interactionUnlockData = new Dictionary<float, InteractionData>();
        foreach (NeedinessReq entry in needinessToUnlockedInteractions)
        {
            interactionUnlockData.Add(entry.needinessRequirement,
                entry.unlockedInteraction.GetComponent<InteractionData>());
        }

        unlockValues = new List<float>(interactionUnlockData.Keys);
        unlockValues.Sort();
        unlockIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (unlockIndex < unlockValues.Count)
        {
            float nextUnlockValue = unlockValues[unlockIndex];
            if (petStatus.getNeediness() >= nextUnlockValue)
            {
                InteractionData newInteraction =
                    interactionUnlockData[nextUnlockValue];
                interactionManager.startInteractionSystem(newInteraction);
                unlockIndex += 1;
            }
        }
    }
}
