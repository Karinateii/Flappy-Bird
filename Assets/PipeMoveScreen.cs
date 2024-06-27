using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScreen : MonoBehaviour
{
    // Speed at which the pipes move left
    public float moveSpeed = 10;
    // Position on the x-axis where pipes will be destroyed
    public float deadZone = -45;

    // Start is called before the first frame update
    void Start()
    {
        // Intentionally left blank
    }

    // Update is called once per frame
    void Update()
    {
        // Move the pipe to the left based on moveSpeed and deltaTime
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        // Check if the pipe has moved past the dead zone
        if (transform.position.x < deadZone)
        {
            // Log a message for debugging
            Debug.Log("Pipe Deleted");
            // Destroy the pipe GameObject
            Destroy(gameObject);
        }
    }
}
