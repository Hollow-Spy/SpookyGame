using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
  //  [SerializeField] float mag, freq, dura;
    Vector3 OGPos;
    private void Start()
    {
        OGPos = transform.localPosition;
    }
    //cam hit default: 0.16f ; 0.021f ; 0.35f

   /* private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShakeScreen(mag,freq,dura);
        }
    }
   */

    public void ShakeScreen(float magnitude, float frequency, float duration)
    {
        StartCoroutine(ShakeDuration(magnitude,frequency,duration ) );
    }

  

    IEnumerator ShakeDuration(float magnitude, float frequency, float duration)
    {
        float TimeLapsed=0;
        float FrequencyTick=0;

        while(TimeLapsed < duration )
        {
            yield return null;
            TimeLapsed += Time.deltaTime;

            FrequencyTick += Time.deltaTime;
            if(FrequencyTick >= frequency)
            {
                FrequencyTick = 0;
                transform.localPosition = new Vector3(OGPos.x + Random.Range(-1, 1) * magnitude, OGPos.y + Random.Range(-1,1 ) * magnitude, OGPos.z);
            }
        }
        transform.localPosition = OGPos;

    }

}
