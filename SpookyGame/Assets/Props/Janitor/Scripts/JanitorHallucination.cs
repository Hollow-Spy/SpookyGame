using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorHallucination : MonoBehaviour
{
    [SerializeField] JumpscareFOV fov;
    [SerializeField] Collider colliderObj;
    bool active;
    [SerializeField] float WaitTilFade;
    [SerializeField] GameObject ScarySound;

    private void OnEnable()
    {
        active = true;
    }

    private void Update()
    {
        if(fov.Refs.Count > 0)
        {
          if(fov.CheckVision(colliderObj.transform) && active)
            {
                active = false;
                StartCoroutine(FadeAway());
            }
        }
    }

    IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(WaitTilFade);
        Instantiate(ScarySound, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }


}
