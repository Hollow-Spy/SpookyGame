using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutscene : MonoBehaviour
{
    GameObject player;
    [SerializeField] PhoneTutorial phone;
   public void DisablePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.transform.parent.gameObject;
        player.SetActive(false);
    }
    public void EnablePlayer()
    {
        player.SetActive(true);
    }

    public void RingPhone()
    {
        phone.enabled = true;
    }
}
