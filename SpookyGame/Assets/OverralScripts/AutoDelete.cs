using UnityEngine;

public class AutoDelete : MonoBehaviour
{
    [SerializeField] float time;
    void Start()
    {
        Destroy(gameObject, time);
        
    }

   
}
