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
    void instantiateInteractions();

    // Can add other things that all interaction systems should have later; for
    // instance, maybe a way to update neediness influences
}
