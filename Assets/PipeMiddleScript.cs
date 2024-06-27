using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    // Reference to the LogicScript
    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        // Find the LogicScript component tagged as "Logic"
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Intentionally left blank
    }

    // Trigger event when another collider enters this trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is in layer 3 (Player layer)
        if (collision.gameObject.layer == 3)
        {
            // Add score when the bird passes through the middle of the pipes
            logic.addScore(1);
        }
    }
}
