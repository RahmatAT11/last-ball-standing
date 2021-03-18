using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
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

    // Camera move as the focus point move
    void FollowPlayer()
    {
        transform.position = player.transform.position;
    }

    // Camera rotates around the focus point
    void CameraRotate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotateSpeed * horizontalInput * Time.deltaTime);
    }
}
