using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject focusPoint;

    private PlayerControl playerControl;

    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        GetAllObjectNeeded();
    }

    private void GetAllObjectNeeded()
    {
        focusPoint = GameObject.Find("Focus Point");

        playerControl = GetComponent<PlayerControl>();

        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        RollPlayer();
    }

    // Roll player with WS keys
    void RollPlayer()
    {
        // Roll direction
        Vector3 rollDirection = focusPoint.transform.forward;

        // Roll the ball forward and backward
        playerRb.AddForce(rollDirection * playerControl.RollForce * playerControl.VerticalInput);
    }
}
