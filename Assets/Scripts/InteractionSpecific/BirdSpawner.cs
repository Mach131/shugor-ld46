using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Takes care of generating birds every so often
public class BirdSpawner : MonoBehaviour
{
    public BirdInteraction bird;

    [SerializeField]
    private float MIN_SPAWN_INTERVAL = 1.5f;
    [SerializeField]
    private float MAX_SPAWN_INTERVAL = 6;
    [SerializeField]
    private float SPAWN_HEIGHT_RANGE = 0.75f;
    [SerializeField]
    private float SPAWN_DEPTH_RANGE = 0.5f;

    private float birdSpawnTimer;

    private PetStatus petStatus;
    private string conditionName;
    private float conditionRestoreAmount;
    private float needinessIncreaseAmount;
    private BirdlessnessInteractionData interactionData;

    // Start is called before the first frame update
    void Start()
    {
        spawnAndResetTimer();
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

    private void spawnAndResetTimer()
    {
        float spawn_height = transform.position.y + Random.Range(
            -SPAWN_HEIGHT_RANGE, SPAWN_HEIGHT_RANGE);
        float spawn_depth = transform.position.z + Random.Range(
            -SPAWN_DEPTH_RANGE, SPAWN_DEPTH_RANGE);

        BirdInteraction newBird = Instantiate(
            bird.gameObject, new Vector3(0, spawn_height, spawn_depth),
            Quaternion.identity, transform).GetComponent<BirdInteraction>();
        newBird.Initialize(petStatus, conditionName, conditionRestoreAmount,
            needinessIncreaseAmount, interactionData);

        birdSpawnTimer = Random.Range(MIN_SPAWN_INTERVAL, MAX_SPAWN_INTERVAL);
    }

    // Update is called once per frame
    void Update()
    {
        birdSpawnTimer -= Time.deltaTime;
        if (birdSpawnTimer < 0)
        {
            spawnAndResetTimer();
        }
    }
}
