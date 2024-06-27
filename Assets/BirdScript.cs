using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody; // Reference to the bird's Rigidbody2D component
    public float manualFlapStrength = 15f; // Flap strength when controlled manually
    public float autonomousFlapStrength = 4.5f; // Flap strength when controlled by AI
    public float manualGravityScale = 5f; // Gravity scale when controlled manually
    public float autonomousGravityScale = 45f; // Gravity scale when controlled by AI
    public LogicScript logic; // Reference to the logic script
    public bool birdIsAlive = true; // Flag to check if the bird is alive
    public bool isAutonomous = false; // Flag to check if the bird is controlled by AI

    private AutonomousBird autonomousBird; // Reference to the AI control script

    void Start()
    {
        // Get references to the necessary components
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        myRigidbody = GetComponent<Rigidbody2D>();
        autonomousBird = GetComponent<AutonomousBird>();
        SetMode(isAutonomous);  // Set the initial mode (manual or autonomous)
    }

    void Update()
    {
        if (birdIsAlive)
        {
            // Manual control: flap when the space key is pressed
            if (Input.GetKeyDown(KeyCode.Space) && !isAutonomous)
            {
                myRigidbody.velocity = Vector2.up * manualFlapStrength;
            }
            // Autonomous control: navigate through pipes
            else if (isAutonomous)
            {
                autonomousBird.NavigateThroughPipes();
            }
        }

        // Toggle between manual and autonomous modes for testing (e.g., using the "M" key)
        if (Input.GetKeyDown(KeyCode.M))
        {
            isAutonomous = !isAutonomous;
            SetMode(isAutonomous);
        }
    }

    // Method to switch between manual and autonomous modes
    void SetMode(bool autonomous)
    {
        if (autonomous)
        {
            myRigidbody.gravityScale = autonomousGravityScale;
            autonomousBird.enabled = true;
            autonomousBird.flapStrength = autonomousFlapStrength;
        }
        else
        {
            myRigidbody.gravityScale = manualGravityScale;
            autonomousBird.enabled = false;
        }
    }

    // Handle collision with other objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        birdIsAlive = false;
    }
}
