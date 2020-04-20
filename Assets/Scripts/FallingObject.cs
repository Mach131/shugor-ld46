using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the motion of objects that should just be falling down.
/// </summary>
public class FallingObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 GRAVITY = new Vector3(0, -16, 0);
    [SerializeField]
    private float OFFSCREEN_THRESHOLD = -20;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        velocity += GRAVITY * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        if (transform.position.y <= OFFSCREEN_THRESHOLD)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
