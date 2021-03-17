using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float rotateSpeed = 200.0f;
    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotate();
    }

    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // Camera move as the focus point move
        transform.position = player.transform.position;
    }

    void CameraRotate()
    {
        // Camera rotates around the focus point
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotateSpeed * horizontalInput * Time.deltaTime);
    }
}
