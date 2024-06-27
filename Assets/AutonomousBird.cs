using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AutonomousBird : MonoBehaviour
{
    public Rigidbody2D myRigidbody; // Reference to the bird's Rigidbody2D component for physics-based movement
    public float flapStrength; // The strength of each flap
    public float maxFlapStrength; // Maximum strength of a flap to limit the upward force
    public float minFlapStrength; // Minimum strength of a flap to ensure some upward force
    public List<Transform> pipes; // List to store the transforms of the pipes
    public bool birdIsAlive = true; // Flag to check if the bird is alive

    void Start()
    {
        // Initialize the list of pipes
        pipes = new List<Transform>();
    }

    // Method to navigate through the pipes
    public void NavigateThroughPipes()
    {
        // Check if there are pipes and if the Rigidbody2D component is assigned
        if (pipes.Count > 0 && myRigidbody != null)
        {
            // Filter out any null pipes and their game objects
            pipes = pipes.Where(pipe => pipe != null && pipe.gameObject != null).ToList();

            // If no valid pipes are found, log a warning and exit the method
            if (pipes.Count == 0)
            {
                Debug.LogWarning("No valid pipes to navigate through.");
                return;
            }

            // Sort the pipes by distance from the bird
            pipes = pipes.OrderBy(pipe => Vector3.Distance(transform.position, pipe.position)).ToList();
            Transform closestPipe = pipes[0]; // Get the closest pipe

            // Check if the closest pipe is valid
            if (closestPipe != null)
            {
                Collider2D pipeCollider = closestPipe.GetComponent<Collider2D>(); // Get the pipe's Collider2D component
                if (pipeCollider != null)
                {
                    // Calculate the target Y position to aim for the middle of the pipe gap
                    float targetY = (closestPipe.position.y - pipeCollider.bounds.extents.y) + (pipeCollider.bounds.size.y / 2);
                    
                    // Calculate the flap power based on the distance to the target Y position
                    float flapPower = Mathf.Clamp((targetY - transform.position.y) * flapStrength, minFlapStrength, maxFlapStrength);

                    // Apply upward velocity if the bird is below the target Y position
                    if (transform.position.y < targetY)
                    {
                        myRigidbody.velocity = Vector2.up * flapPower;
                    }
                }
                else
                {
                    Debug.LogWarning("Collider2D component not found on pipe GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("Closest pipe is null or destroyed.");
            }
        }
    }
}
