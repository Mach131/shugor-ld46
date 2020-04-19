using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An interface for some functionality that interaction systems should be able
/// to handle. For reference, a single "interaction system" refers to the components
/// involved in maintaining a single one of the pet's conditions.
/// </summary>
public interface InteractionData
{
    /// <summary>
    /// Adds the condition that this interaction system is connected to to the
    /// petStatus object.
    /// </summary>
    /// <param name="petStatus">Pet's status object</param>
    void addCondition(PetStatus petStatus);

    /// <summary>
    /// Instantiates all of the objects required for this interaction system to work.
    /// </summary>
    /// <param name="petStatus">Pet's status object</param>
    /// <param name="transform">The parent transform to instantiate objects
    /// under.</param>
    void instantiateInteractions(PetStatus petStatus);

    // Can add other things that all interaction systems should have here, then use
    // the InteractionDataManager to call them
}
