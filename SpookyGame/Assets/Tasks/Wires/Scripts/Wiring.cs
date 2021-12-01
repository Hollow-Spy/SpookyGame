
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiring : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    GameObject target;
    public bool canMove;
    Vector3 screenPosition;
    Vector3 offset;
    bool isDragged;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        Physics.IgnoreCollision(GetComponent<BoxCollider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>(), true);
    }

    public void SetMaterial(Material material)
    {
        meshRenderer.material = material;
    }
    public Material GetMaterial()
    {
       return meshRenderer.material;
    }

}
