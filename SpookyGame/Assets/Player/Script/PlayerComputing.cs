using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComputing : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject mainpuzzle;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

    }

    public void Win()
    {
        GameObject.FindGameObjectWithTag("Computer").SendMessage("TaskComplete",false);
      
    }

    public void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerZoom>().ReturnPlayer();
        Destroy(gameObject);

    }
}
