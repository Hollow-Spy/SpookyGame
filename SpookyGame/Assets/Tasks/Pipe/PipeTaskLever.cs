using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTaskLever : MonoBehaviour
{
    bool Pressing;
    [SerializeField] GameObject mainCam;
    [SerializeField] LayerMask InteractiveLayer;
    [SerializeField] ConfigurableJoint joint;

    [SerializeField] PipeTaskMeter Meter;

    [SerializeField] GameObject GreenLight, RedLight, GrabSound,CreakSound,ClickSound,WarningSound;

    bool CreakOnce = false;
    bool ClickOnce = false;
    public void Interaction()
    {
        Pressing = true;
        Instantiate(GrabSound, transform.position, Quaternion.identity);

    }
    private void Update()
    {
        RaycastHit hit;
        if (Pressing)
        {
            if (Input.GetMouseButton(0) && Physics.SphereCast(mainCam.transform.position, .3f, mainCam.transform.forward, out hit, 2, InteractiveLayer) && hit.transform.gameObject == gameObject)
            {
                if (mainCam.transform.rotation.x > 0)
                {
                    joint.targetRotation = new Quaternion(0, 0, mainCam.transform.rotation.x, 0);
                }

                if(transform.eulerAngles.z < 190)
                {

                    if (transform.eulerAngles.z > 90 && !CreakOnce)
                    {
                        CreakOnce = true;
                        Instantiate(CreakSound, transform.position, Quaternion.identity);
                    }

                    if (transform.eulerAngles.z > 150)
                    {
                        GreenLight.SetActive(true);
                        Meter.ReducePressure();
                        Meter.busy = true;
                        if (!ClickOnce)
                        {
                            ClickOnce = true;
                            Instantiate(ClickSound, transform.position, Quaternion.identity);

                        }
                    }
                    else
                    {
                        ClickOnce = true;
                        GreenLight.SetActive(false);
                    }
                }


            }
            else
            {
                 ButtonUnpressed();
                Meter.busy = false;
            }
        }



    }

    IEnumerator FlashRedLight()
    {
        Instantiate(WarningSound, transform.position, Quaternion.identity);
        RedLight.SetActive(true);
        yield return new WaitForSeconds(1);
        RedLight.SetActive(false);

    }
    public void PressureBroke()
    {
      
        StartCoroutine(FlashRedLight());
        mainCam.GetComponent<CameraShake>().ShakeScreen(0.06f, 0.021f, 0.25f);
        ButtonUnpressed();
    }

    void ButtonUnpressed()
    {
        Meter.busy = false;
        ClickOnce = false;
        CreakOnce = false;
        Pressing = false;
        joint.targetRotation = Quaternion.identity;
        Meter.ResetPressureTimer();
        GreenLight.SetActive(false);
        //  Instantiate(UnpressSound, transform.position, Quaternion.identity);
        //  ButtonIn.SetActive(false);
        //  ButtonOut.SetActive(true);
    }


}