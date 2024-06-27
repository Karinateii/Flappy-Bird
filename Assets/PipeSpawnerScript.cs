using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    // The pipe prefab to spawn
    public GameObject pipe;
    // Rate at which pipes are spawned
    public float spawnRate = 1;
    // Timer to track time between spawns
    private float timer = 0;
    // Vertical offset to randomize pipe spawn height
    public float heightOffset = 10;

    // Reference to the AutonomousBird script
    private AutonomousBird autonomousBird;

    // Start is called before the first frame update
    void Start()
    {
        // Find the AutonomousBird script in the scene
        autonomousBird = FindObjectOfType<AutonomousBird>();

        // Spawn the first pipe immediately
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the timer by the time elapsed since the last frame
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            // If timer exceeds spawnRate, spawn a new pipe and reset the timer
            spawnPipe();
            timer = 0;
        }
    }

    // Method to spawn a new pipe
    void spawnPipe()
    {
        // Calculate the lowest and highest points for pipe spawn height
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        // Instantiate a new pipe at a random height within the calculated range
        GameObject newPipe = Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), Quaternion.identity);

        // Add the new pipe to the AutonomousBird's list of pipes for AI navigation
        if (autonomousBird != null)
        {
            autonomousBird.pipes.Add(newPipe.transform);
        }
    }
};
