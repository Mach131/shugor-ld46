using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls the player's interactions with a water bucket.
/// </summary>
public class WaterBucketInteraction : MonoBehaviour
{
    private static Vector3 DEFAULT_POSITION = new Vector3(-10, -5, 0);
    private static float FILL_RATE = 33;
    private static float OVERFULL_THRESHOLD = 120;
    private static float EMPTYING_TIME = 1.33f;
    private static float FULLNESS_SCALING_FACTOR = 0.8f;

    public WaterDrop waterDrop;

    private float fullness;
    private bool overfull;
    private bool filling;
    private bool emptying;
    private float emptyingTimer;

    private TextMeshPro textMesh;
    private DraggableObject drag;
    public Transform FillBar;
    private Renderer FillRender;
    public Transform SelectParticles;

    private PetStatus petStatus;
    private string conditionName;
    private float maxConditionRestoreAmount;
    private float needinessIncreaseAmount;

    private void Start()
    {
        fullness = 0;
        overfull = false;
        filling = true;
        emptying = false;
        emptyingTimer = 0;

        drag = GetComponent<DraggableObject>();
        textMesh = GetComponentInChildren<TextMeshPro>();
        FillRender = FillBar.GetComponent<Renderer>();
        updateText();
    }

    public void Initialize(PetStatus petStatus, string conditionName,
        float conditionRestoreAmount, float needinessIncreaseAmount)
    {
        this.petStatus = petStatus;
        this.conditionName = conditionName;
        this.maxConditionRestoreAmount = conditionRestoreAmount;
        this.needinessIncreaseAmount = needinessIncreaseAmount;

        transform.position = DEFAULT_POSITION;
    }

    private void updateText()
    {
        int displayFullness = (int) Mathf.Min(fullness, 100);
        string overflowing = overfull ? "+" : "";
        textMesh.text = displayFullness.ToString() + "%" + overflowing;
    }

    private void Update()
    {
        FillRender.material.SetFloat("_Cutoff", 1f - Mathf.Min(fullness, 100)/100);
        if (filling && !overfull)
        {
            fullness += FILL_RATE * Time.deltaTime;
            if (fullness >= OVERFULL_THRESHOLD)
            {
                overfull = true;
            }
            updateText();
        }

        else if (emptying)
        {
            emptyingTimer -= Time.deltaTime;
            if (emptyingTimer <= 0)
            {
                transform.position = DEFAULT_POSITION;
                fullness = 0;
                overfull = false;
                emptying = false;
                filling = true;
            }
        }
    }

    // Called when dragging begins
    public void pickUp()
    {
        Instantiate(SelectParticles, transform.position+new Vector3(0.0f,0.0f,-1f),Quaternion.Euler(0f,0f,0f));
        if (filling)
        {
            filling = false;
        } else
        {
            drag.cancelDrag();
        }
    }

    // Called when dragging ends
    public void startEmptying()
    {
        float conditionRestoreAmount = fullness >= 100 ?
            maxConditionRestoreAmount :
            (maxConditionRestoreAmount * FULLNESS_SCALING_FACTOR * fullness) / 100;

        GameObject newDrop = GameObject.Instantiate(waterDrop.gameObject, transform);
        newDrop.GetComponent<WaterDrop>().Initialize(petStatus, conditionName,
            conditionRestoreAmount, needinessIncreaseAmount);

        emptying = true;
        emptyingTimer = EMPTYING_TIME;
    }

    public bool isOverfull()
    {
        return overfull;
    }
}
