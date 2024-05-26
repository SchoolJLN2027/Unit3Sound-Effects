using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerControllerScript;
    float speed = 30;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false && playerControllerScript.dash == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if(playerControllerScript.gameOver == false && playerControllerScript.dash == true)
        {
            transform.Translate(Vector3.left * (speed * 1.5f) * Time.deltaTime);
        }
    }
}
