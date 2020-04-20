using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles interactions with the game console.
/// </summary>
public class SwitchGameInteraction : MonoBehaviour
{
    public Transform GamingParticle;

    [SerializeField]
    private float ROOM_EXTENTS = 4.75f;
    [SerializeField]
    private float GROUNDED_DRAG = 2f;
    [SerializeField]
    private float MINIMUM_GAMING_TIME = 2f;
    [SerializeField]
    private float MAXIMUM_GAMING_TIME = 8f;
    [SerializeField]
    private float VERTICAL_THROW_SPEED = 16f;
    [SerializeField]
    private float HORIZONTAL_THROW_EXTENTS = 16f;

    private bool playerHolding;
    private bool petHolding;
    private bool petCanCatch;
    private float hspeed;
    private float gamingTimer;

    private PetStatus petStatus;
    private string conditionName;
    private float conditionRestorePerSecond;
    private float needinessIncreasePerSecond;

    private Collider coll;
    private Collider petColl;
    private FallingObject faller;
    private Transform currentParticle;

    // Start is called before the first frame update
    void Start()
    {
        playerHolding = false;
        petHolding = false;
        petCanCatch = false;
        hspeed = 0;
        gamingTimer = 0;

        coll = GetComponentInChildren<Collider>();
        faller = GetComponent<FallingObject>();
    }

    public void Initialize(PetStatus petStatus, string conditionName,
        float conditionRestorePerSecond, float needinessIncreasePerSecond)
    {
        this.petStatus = petStatus;
        this.conditionName = conditionName;
        this.conditionRestorePerSecond = conditionRestorePerSecond;
        this.needinessIncreasePerSecond = needinessIncreasePerSecond;

        this.petColl = petStatus.GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHolding)
        {
            if (petHolding)
            {
                // Increase value here
                float additionalValue = conditionRestorePerSecond * Time.deltaTime;
                float additionalNeediness =
                    needinessIncreasePerSecond * Time.deltaTime;
                petStatus.increaseConditionValue(conditionName, additionalValue);
                petStatus.increaseNeediness(additionalNeediness);
                
                if (currentParticle == null)
                {
                    Vector2 gamingDisp = Random.insideUnitCircle;
                    currentParticle = Instantiate(GamingParticle,
                        transform.position + new Vector3(
                            gamingDisp.x, gamingDisp.y, -1f),
                        Quaternion.Euler(0f, 0f, 0f));
                }

                gamingTimer -= Time.deltaTime;
                if (gamingTimer <= 0)
                {
                    petHolding = false;
                    faller.enabled = true;
                    faller.setVelocity(Vector3.up * VERTICAL_THROW_SPEED);
                    hspeed = Random.Range(
                        -HORIZONTAL_THROW_EXTENTS, HORIZONTAL_THROW_EXTENTS);
                }
            } else if (petCanCatch)
            {
                if (Utils.checkOverlap(coll, petColl))
                {
                    Vector3 petPosition = petColl.transform.position;
                    transform.position = new Vector3(
                        petPosition.x, petPosition.y, transform.position.z);
                    petCanCatch = false;
                    petHolding = true;
                    faller.enabled = false;
                    gamingTimer = Random.Range(
                        MINIMUM_GAMING_TIME, MAXIMUM_GAMING_TIME);
                }
            } else
            {
                if (hspeed != 0)
                {
                    transform.position += Vector3.right * hspeed * Time.deltaTime;

                    if ((hspeed < 0 && transform.position.x < -ROOM_EXTENTS) ||
                        (hspeed > 0 && transform.position.x > ROOM_EXTENTS))
                    {
                        transform.position = new Vector3(
                            ROOM_EXTENTS * Mathf.Sign(hspeed), transform.position.y,
                            transform.position.z);
                        hspeed *= -1;
                    }

                    if (transform.position.y <= faller.getThreshold() + 0.5f)
                    {
                        float drag = GROUNDED_DRAG * Time.deltaTime;
                        if (Mathf.Abs(hspeed) < drag)
                        {
                            hspeed = 0;
                        } else
                        {
                            hspeed -= (drag * Mathf.Sign(hspeed));
                        }
                    }
                }
            }
        }
    }

    // called on click
    public void pickedUp()
    {
        petHolding = false;
        faller.enabled = false;
        playerHolding = true;
    }

    // called on release
    public void dropped()
    {
        playerHolding = false;
        faller.enabled = true;
        petCanCatch = true;
    }
}
