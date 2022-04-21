using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; } = new PlayerInfo();
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public float Stemina { get; set; }
    public float ColdGuage { get; set; } = 0;
    public float MaxColdGuage { get; set; } = 15;
    public float Battery { get; set; } = 5;
    public float MaxBattery { get; set; } = 5;
    public bool IsRanding
    {
        get
        {
            if (!playerController)
            {
                GameObject player = GameObject.Find("Player");
                playerController = player.GetComponent<PlayerController>();
            }

            return playerController.isGround;
        }
        set
        {
            if (!playerController)
            {
                GameObject player = GameObject.Find("Player");
                playerController = player.GetComponent<PlayerController>();
            }

            playerController.isGround = value;
        }
    }
    public List<GameObject> Items { get; set; }
    public GameObject Item { get; set; }

    PlayerController playerController = null;
}