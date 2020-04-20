using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player's interactions with the sweets pile.
/// </summary>
public class SweetsPileInteraction : MonoBehaviour
{
    public SweetPieceInteraction sweetsPrefab;
    public Transform SelectParticles;

    private PetStatus petStatus;
    private string conditionName;
    private float conditionRestoreValue;
    private float needinessIncreaseAmount;

    public void Initialize(PetStatus petStatus, string conditionName,
        float conditionRestoreValue, float needinessIncreaseAmount)
    {
        this.petStatus = petStatus;
        this.conditionName = conditionName;
        this.conditionRestoreValue = conditionRestoreValue;
        this.needinessIncreaseAmount = needinessIncreaseAmount;
    }

    // function that gets called when you click on this
    public void attatchSweetToCursor()
    {
        Instantiate(SelectParticles,
            transform.position + new Vector3(0.0f, 0.0f, -1f),
            Quaternion.Euler(0f, 0f, 0f));
        GameObject newSweet = Instantiate(sweetsPrefab.gameObject,
            transform.position + new Vector3(0, 0, -(transform.position.z + 1)),
            Quaternion.Euler(0, 0, 0));
        SweetPieceInteraction newSweetInteraction =
            newSweet.GetComponent<SweetPieceInteraction>();
        newSweetInteraction.Initialize(petStatus, conditionName,
            conditionRestoreValue, needinessIncreaseAmount);
    }
}
