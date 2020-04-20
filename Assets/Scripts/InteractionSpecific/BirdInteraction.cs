using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls interactions with the bird.
/// </summary>
public class BirdInteraction : MonoBehaviour
{
    [Serializable]
    public struct BirdFlapSprites
    {
        public Sprite flap1;
        public Sprite flap2;
    }

    public Transform birdParticle;
    [SerializeField]
    private float ROOM_EXTENTS = 6.0f;
    [SerializeField]
    private float SECONDS_BETWEEN_FLAPS = 0.6f;
    [SerializeField]
    public List<BirdFlapSprites> birdFlapSprites;

    private float flapTimer;
    private bool firstFlap;
    private bool movingRight;
    private float speed;

    private BirdFlapSprites chosenFlapSprites;
    private SpriteRenderer rend;

    private PetStatus petStatus;
    private string conditionName;
    private float conditionRestoreAmount;
    private float needinessIncreaseAmount;
    private BirdlessnessInteractionData interactionData;

    // Start is called before the first frame update
    void Start()
    {
        flapTimer = 0;
        firstFlap = true;
        rend = GetComponentInChildren<SpriteRenderer>();

        int chosenFlapIndex = UnityEngine.Random.Range(0, birdFlapSprites.Count);
        chosenFlapSprites = birdFlapSprites[chosenFlapIndex];
        rend.sprite = chosenFlapSprites.flap1;

        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            movingRight = true;
            transform.position = new Vector3(-ROOM_EXTENTS,
                transform.position.y, transform.position.z);
        } else
        {
            movingRight = false;
            transform.position = new Vector3(ROOM_EXTENTS,
                transform.position.y, transform.position.z);
        }
        rend.flipX = !movingRight;
        speed = UnityEngine.Random.Range(ROOM_EXTENTS / 3, ROOM_EXTENTS);
    }
    
    public void Initialize(PetStatus petStatus, string conditionName,
        float conditionRestoreAmount, float needinessIncreaseAmount,
        BirdlessnessInteractionData interactionData)
    {
        this.petStatus = petStatus;
        this.conditionName = conditionName;
        this.conditionRestoreAmount = conditionRestoreAmount;
        this.needinessIncreaseAmount = needinessIncreaseAmount;
        this.interactionData = interactionData;
    }

    // Update is called once per frame
    void Update()
    {
        flapTimer += Time.deltaTime;
        if (flapTimer >= SECONDS_BETWEEN_FLAPS)
        {
            flapTimer = 0;
            firstFlap = !firstFlap;

            rend.sprite = firstFlap ?
                chosenFlapSprites.flap1 :
                chosenFlapSprites.flap2;
        }

        Vector3 direction = movingRight ?
            Vector3.right : Vector3.left;
        transform.position += direction * speed * Time.deltaTime;
        if ((movingRight && transform.position.x >= ROOM_EXTENTS) ||
            (!movingRight && transform.position.x <= -ROOM_EXTENTS))
        {
            this.interactionData.birdMissed();
            GameObject.Destroy(gameObject);
        }
    }

    // called when clicked
    public void onClick()
    {
        this.petStatus.increaseConditionValue(conditionName, conditionRestoreAmount);
        this.petStatus.increaseNeediness(needinessIncreaseAmount);
        Instantiate(birdParticle,
            transform.position + new Vector3(0f, 0f, 0f),
            Quaternion.Euler(0f, 0f, 0f));
        this.interactionData.birdHit();
        GameObject.Destroy(gameObject);
    }
}
