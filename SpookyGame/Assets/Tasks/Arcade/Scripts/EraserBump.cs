using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserBump : MonoBehaviour
{
    [SerializeField] EraserGame game;
    [SerializeField] GameObject crashSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Arcade"))
        {
            Instantiate(crashSound, transform.position, Quaternion.identity);
            game.StopGame();
            
        }
    }
}
