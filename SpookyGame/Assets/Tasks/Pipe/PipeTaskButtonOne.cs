using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTaskButtonOne : MonoBehaviour
{
    bool Pressing;
    [SerializeField] GameObject mainCam;
    [SerializeField] LayerMask InteractiveLayer;
    [SerializeField] GameObject ButtonIn, ButtonOut, PressSound, UnpressSound;
    [SerializeField] PipeTaskMeter Meter;
    [SerializeField] ParticleSystem Sparkles;
    [SerializeField] GameObject SparkSound;
 
  public void Interaction()
    {
        Pressing = true;
        Instantiate(PressSound, transform.position, Quaternion.identity);
        ButtonIn.SetActive(true);
        ButtonOut.SetActive(false);
       
    }
    private void Update()
    {
        RaycastHit hit;
        if(Pressing)
        {
            if (Input.GetMouseButton(0) && Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, 2, InteractiveLayer) && hit.transform.gameObject == gameObject)
            {
                Meter.ReducePressure();
                Meter.busy = true;
            }
            else
            {
                ButtonUnpressed();
                Meter.busy = false;

            }
        }
     
      

    }


    void ButtonUnpressed()
    {
        Meter.busy = false;
        Pressing = false;
        Meter.ResetPressureTimer();
        Instantiate(UnpressSound, transform.position, Quaternion.identity);
        ButtonIn.SetActive(false);
        ButtonOut.SetActive(true);
    }

    public void PressureBroke()
    {
        Sparkles.Play();
        Instantiate(SparkSound, transform.position, Quaternion.identity);
        mainCam.GetComponent<CameraShake>().ShakeScreen(0.06f, 0.021f, 0.25f);
        ButtonUnpressed();
    }


    private void OnEnable()
    {
        Pressing = false;
    }
}
