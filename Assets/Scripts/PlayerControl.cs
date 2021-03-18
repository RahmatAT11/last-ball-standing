using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float VerticalInput { get; private set; }
    public float RollForce { get; private set; }
    public float BouncePower { get; private set; }

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        RollForce = 12f;
        BouncePower = 20f;
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PauseGameInput();
        GetVerticalInput();
        PlayerFall();
    }

    // Get the input for RollPlayer method
    void GetVerticalInput()
    {
        if (!GameManager.isPause)
        {
            // Player can roll around using the WASD key
            VerticalInput = Input.GetAxis("Vertical");
        }
    }

    void PauseGameInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.isPause = !GameManager.isPause;
            gameManager.PauseGame();
        }
    }
    
    void PlayerFall()
    {
        float yBound = 5f;
        if (gameObject.transform.position.y < -yBound)
        {
            GameManager.isOver = !GameManager.isOver;
            gameManager.GameOver(0);
        }
    }
}
