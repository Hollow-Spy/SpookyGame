using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : MonoBehaviour
{
    Animator HandAnimator;
    Transform POS;
    public GameObject Sodatschk;
    Rigidbody Body;
    // Start is called before the first frame update
    void Start()
    {
        HandAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<Animator>();
        POS = GameObject.Find("SodaPoint").transform;
        Body = GetComponent<Rigidbody>();
    }
    public void Interaction()
    {
        gameObject.layer = 9;
        Physics.IgnoreCollision(GetComponent<MeshCollider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>(), true);
        Body.isKinematic = true;
        transform.SetParent(POS);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        HandAnimator.SetTrigger("drink");
        StartCoroutine(tschk());

    }

    IEnumerator tschk()
    {
        yield return new WaitForSeconds(.1f);
        Instantiate(Sodatschk, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Tiredness>().isTired = false;
        Destroy(gameObject);
    }
}
