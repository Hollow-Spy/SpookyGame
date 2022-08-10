using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzle : MonoBehaviour
{
    public List<Material> wireColors = new List<Material>();

    public List<Wiring> jacks = new List<Wiring>();

    public List<Wiring> plugs = new List<Wiring>();

    private List<Material> remainingColors;

    private List<Wiring> remainingJackIndex;

    private List<Wiring> remainingPlugIndex;

    GameObject target;

    GameObject matchingTarget;

    Vector3 screenPosition;

    Vector3 offset;

    bool isDragged;

    Material jackMat;

    Material plugMat;

    int matchCount;

    [SerializeField] Vector3[] spawnPosition = new Vector3[4];

    // Start is called before the first frame update
    private void OnEnable()
    {
        matchCount = 0;
        remainingColors = new List<Material>(wireColors);
        remainingJackIndex = new List<Wiring>();
        remainingPlugIndex = new List<Wiring>();

        for (int i = 0; i < jacks.Count; i++)
        {
            remainingJackIndex.Add(jacks[i]);
            jacks[i].transform.position = spawnPosition[i];
            jacks[i].canMove = true;
        }
        for (int i = 0; i < plugs.Count; i++) { remainingPlugIndex.Add(plugs[i]); }

        //While there are remaining entries in all lists this will run 
        while (remainingColors.Count > 0 && remainingJackIndex.Count > 0 && remainingPlugIndex.Count > 0)
        {
            Material chosenColor = remainingColors[UnityEngine.Random.Range(0, remainingColors.Count)];
            Wiring chosenJack = remainingJackIndex[UnityEngine.Random.Range(0, remainingJackIndex.Count)];
            Wiring chosenPlug = remainingPlugIndex[UnityEngine.Random.Range(0, remainingPlugIndex.Count)];

            chosenJack.SetMaterial(chosenColor);
            chosenPlug.SetMaterial(chosenColor);

            Debug.Log(chosenColor + " is assigned to " + chosenJack + " and " + chosenPlug);

            remainingPlugIndex.Remove(chosenPlug);
            remainingJackIndex.Remove(chosenJack);
            remainingColors.Remove(chosenColor);
        }
    }

    IEnumerator TaskDone(bool failed)
    {
        while (GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().busy)
        {
            yield return null;
        }
        GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().RemoveTask(gameObject, failed);

    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        if (!target.GetComponentInParent<Wiring>())
        {
            target = null;
        }
        else if (target.GetComponentInParent<Wiring>().canMove == false)
        {
            target = null;
        }
        return target;
    }

    GameObject MatchWire(out RaycastHit hit, GameObject jack)
    {
        GameObject plug = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            plug = hit.collider.gameObject;
        }
        if (!plug.GetComponentInParent<Wiring>())
        {
            plug = null;
        }
        else if (plug == jack)
        {
            plug = null;
        }
        return plug;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            target = ReturnClickedObject(out hitInfo);

            if (target != null)
            {
                isDragged = true;
                Debug.Log("target position :" + target.transform.position);

                screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragged = false;
        }

        if (isDragged)
        {
            RaycastHit hitInfo;
            matchingTarget = MatchWire(out hitInfo, target);
            if (matchingTarget != null)
            {
                Debug.Log("Match in progress");
                jackMat = target.GetComponentInChildren<Wiring>().GetMaterial(); ;
                plugMat = matchingTarget.GetComponentInChildren<Wiring>().GetMaterial();
                if (jackMat.name == plugMat.name)
                {
                    target.GetComponentInParent<Wiring>().canMove = false;
                    isDragged = false;
                    Debug.Log("Match Pair Success");
                    matchCount++;

                }
            }
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;
            target.transform.position = new Vector3(currentPosition.x,currentPosition.y,target.transform.position.z);
            if (target.GetComponentInChildren<Wiring>().canMove == false)
            {
                Vector3 snapOffset = new Vector3(-0.05f, 0.0f, 0.0f);
                target.transform.position = matchingTarget.transform.position + snapOffset;
            }
        }
        if (matchCount == 4)
        {

            GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().RemoveTask(gameObject, false);
            this.transform.gameObject.SetActive(false);
        }

    }
}
