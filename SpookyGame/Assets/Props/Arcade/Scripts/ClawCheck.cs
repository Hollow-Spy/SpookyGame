using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawCheck : MonoBehaviour
{
    [SerializeField] ClawMachine machine;
  public void Check()
    {
        machine.Check();
    }
}
