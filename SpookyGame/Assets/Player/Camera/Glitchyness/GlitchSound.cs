using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchSound : MonoBehaviour
{
    [SerializeField] AudioSource audiosource;
    IEnumerator SoundReduceCoroutine,PitchChangeCoroutine;
    [SerializeField] float minPitch, MaxPitch;
  [SerializeField]  float minDistancePitch, maxDistancePitch;
    [SerializeField] GameObject Player, Janitor;
    public void StartSound()
    {
        if(SoundReduceCoroutine != null)
        {
            StopCoroutine(SoundReduceCoroutine);
        }
        if(PitchChangeCoroutine != null)
        {
            StopCoroutine(PitchChangeCoroutine);
        }
    
        audiosource.volume = 1;
        audiosource.Play();

        PitchChangeCoroutine = PitchChangeNumerator();
        StartCoroutine(PitchChangeCoroutine);

    }
    public void StopSound()
    {
        SoundReduceCoroutine = ReduceVolumeNumerator();
        StartCoroutine(SoundReduceCoroutine);
    }

    IEnumerator PitchChangeNumerator()
    {
        while(true)
        {
          
            float distance = Mathf.Clamp( Vector3.Distance(Janitor.transform.position, Player.transform.position),minDistancePitch,maxDistancePitch   );
            Debug.Log ("clamped istance " + distance);

            float normalizeddistance = distance / maxDistancePitch;

           float normalizedPitch = normalizeddistance * MaxPitch;
            Debug.Log(normalizedPitch);

            float currentpitch = Mathf.Clamp( minPitch + MaxPitch - normalizedPitch,minPitch,MaxPitch   );

            


            audiosource.pitch = currentpitch;

            /*  distance = Mathf.Clamp(distance, minDistancePitch, maxDistancePitch);
              Debug.Log(distance);
              float currentPercentageDistance = (distance * MaxPitch) / maxDistancePitch;

            float pitch = Mathf.Clamp((MaxPitch - currentPercentageDistance), minPitch, MaxPitch);
              audiosource.pitch = pitch;*/

            yield return new WaitForSeconds(.5f);
        }
    }
  IEnumerator ReduceVolumeNumerator()
    {
        while (audiosource.volume > 0)
        {
            yield return null;
            audiosource.volume -= 2 * Time.deltaTime;
        }
        audiosource.Stop();
        StopCoroutine(PitchChangeCoroutine);
    }


}
