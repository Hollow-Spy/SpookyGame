using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserBump : MonoBehaviour
{
    [SerializeField] EraserGame game;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Arcade"))
        {
            game.StopGame();
        }
    }
}
