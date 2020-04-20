using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player's interactions with a bouncing ball.
/// </summary>
public class BouncingBallInteraction : MonoBehaviour
{
    // Would be nice to have a more elegant approach to these
    private static float FLOOR_HEIGHT = -3.5f;
    private static float ROOM_EXTENTS = 12;
    
    private static Vector3 GRAVITY = new Vector3(0, -128, 0);
    private static float JUMP_SPEED = 56;
    private static float AIMING_FACTOR = 1.0f;

    // don't feel like using a rigid body altho that might actually be better
    private Vector3 velocity;
    private float z_pos;
    private bool on_ground;
    private bool bounce_active;

    private PetStatus pet_status;
    private string condition_name;
    private float condition_restore_amount;
    private float neediness_increase_amount;

    private Collider pet_coll;
    private Collider coll;

    public Transform SelectParticles;
    public Transform BounceParticles;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        z_pos = transform.position.z;
        on_ground = false;
        bounce_active = false;
    }

    public void Initialize(PetStatus pet_status, string condition_name,
        float condition_restore_amount, float neediness_increase_amount)
    {
        this.pet_status = pet_status;
        this.condition_name = condition_name;
        this.condition_restore_amount = condition_restore_amount;
        this.neediness_increase_amount = neediness_increase_amount;

        this.pet_coll = pet_status.GetComponentInChildren<Collider>();
        this.coll = GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!on_ground)
        {
            velocity += (GRAVITY * Time.deltaTime);
            transform.position = transform.position + (velocity * Time.deltaTime);

            // bouncing
            if (transform.position.x < -ROOM_EXTENTS)
            {
                transform.position = new Vector3(-ROOM_EXTENTS,
                    transform.position.y, transform.position.z);
                velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
            }
            else if (transform.position.x > ROOM_EXTENTS)
            {
                transform.position = new Vector3(ROOM_EXTENTS,
                    transform.position.y, transform.position.z);
                velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
            }

            // landing
            if (transform.position.y <= FLOOR_HEIGHT)
            {
                transform.position = new Vector3(transform.position.x,
                    FLOOR_HEIGHT, transform.position.z);
                on_ground = true;
                bounce_active = false;
                velocity = Vector3.zero;
            }
        }

        if (bounce_active)
        {
            bool checkPetOverlap = Utils.checkOverlap(coll, pet_coll);
            if (checkPetOverlap)
            {
                Instantiate(BounceParticles, transform.position+new Vector3(0.0f,0.0f,-1f),Quaternion.Euler(0f,0f,0f));
                this.pet_status.increaseConditionValue(this.condition_name,
                    this.condition_restore_amount);
                this.pet_status.increaseNeediness(this.neediness_increase_amount);
                Debug.Log("bounce");

                if (velocity.y < 0)
                {
                    velocity *= -1;
                }
                else
                {
                    velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
                }
                bounce_active = false;
            }
        }
    }

    /// <summary>
    /// Called when clicked on; bounces towards the pet.
    /// </summary>
    public void startBounce()
    {
        Instantiate(SelectParticles, transform.position+new Vector3(0.0f,0.0f,-1f),Quaternion.Euler(0f,0f,0f));
        if (!bounce_active)
        {
            //TODO: figure out actual velocity setting?
            Vector3 disp_to_pet = pet_status.transform.position - transform.position;
            velocity = new Vector3(disp_to_pet.x * AIMING_FACTOR, JUMP_SPEED, 0);
            on_ground = false;
            bounce_active = true;
        }
    }
}
