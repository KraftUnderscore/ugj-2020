using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float step;

    private LevelManager levelManager;

    private Transform player1;
    private Transform player2;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        player1 = transform.GetChild(0);
        player2 = transform.GetChild(1);
    }

    private void Start()
    {
        player1.position = levelManager.player1StartPos;
        player2.position = levelManager.player2StartPos;
    }

    // Update is called once per frame
    void Update()
    {
        //0 up, 1 right, 2 down, 3 left
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            bool[] output = levelManager.Move(0);
            if(output[0])
            {
                Vector2 plr1Pos = player1.position;
                player1.position = new Vector2(plr1Pos.x, plr1Pos.y + step);
            }
            if (output[1])
            {
                Vector2 plr2Pos = player2.position;
                player2.position = new Vector2(plr2Pos.x, plr2Pos.y - step);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            bool[] output = levelManager.Move(1);
            if (output[0])
            {
                Vector2 plr1Pos = player1.position;
                player1.position = new Vector2(plr1Pos.x + step, plr1Pos.y);
            }
            if (output[1])
            {
                Vector2 plr2Pos = player2.position;
                player2.position = new Vector2(plr2Pos.x - step, plr2Pos.y);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            bool[] output = levelManager.Move(2);
            if (output[0])
            {
                Vector2 plr1Pos = player1.position;
                player1.position = new Vector2(plr1Pos.x, plr1Pos.y - step);
            }
            if (output[1])
            {
                Vector2 plr2Pos = player2.position;
                player2.position = new Vector2(plr2Pos.x, plr2Pos.y + step);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            bool[] output = levelManager.Move(3);
            if (output[0])
            {
                Vector2 plr1Pos = player1.position;
                player1.position = new Vector2(plr1Pos.x - step, plr1Pos.y);
            }
            if (output[1])
            {
                Vector2 plr2Pos = player2.position;
                player2.position = new Vector2(plr2Pos.x + step, plr2Pos.y);
            }
        }
    }
}
