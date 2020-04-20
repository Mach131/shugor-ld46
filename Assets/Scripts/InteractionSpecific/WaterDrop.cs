using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behavior of the water drop.
/// </summary>
public class WaterDrop : MonoBehaviour
{
    private static Vector3 GRAVITY = new Vector3(0, -16, 0);
    private static float OFFSCREEN_THRESHOLD = -20;

    private Vector3 velocity;

    private PetStatus petStatus;
    private string conditionName;
    private float conditionRestoreAmount;
    private float needinessIncreaseAmount;

    private Collider petColl;
    private Collider coll;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;

        coll = GetComponentInChildren<Collider>();
    }

    public void Initialize(PetStatus petStatus, string conditionName,
        float conditionRestoreAmount, float needinessIncreaseAmount)
    {
        this.petStatus = petStatus;
        this.conditionName = conditionName;
        this.conditionRestoreAmount = conditionRestoreAmount;
        this.needinessIncreaseAmount = needinessIncreaseAmount;

        petColl = petStatus.GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity += GRAVITY * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        if (Utils.checkOverlap(coll, petColl))
        {
            petStatus.increaseConditionValue(conditionName, conditionRestoreAmount);
            petStatus.increaseNeediness(needinessIncreaseAmount);
            //Debug.Log("wosh");
            GameObject.Destroy(gameObject);
        }

        if (transform.position.y <= OFFSCREEN_THRESHOLD)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
