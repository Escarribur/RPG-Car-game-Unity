using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{

    private Rigidbody2D rbody2d;
    private bool gameOver { get; set; }
    private float speed { get; set; }

    void Start()
    {
        gameOver = false;
        speed = -15f;
        rbody2d = GetComponent<Rigidbody2D>();
        updateSpeed();
    }


    void Update()
    {
        if (gameOver == true)
        {
            rbody2d.velocity = Vector2.zero;
        }
    }

    private void updateSpeed()
    {
        rbody2d.velocity = new Vector2(speed, 0);
    }
}
