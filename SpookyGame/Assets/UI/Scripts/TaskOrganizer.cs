using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;


[Serializable]
public class Task
{
  

    [SerializeField] public int Priority;
    [SerializeField] public bool active = true;
    [SerializeField] public string message;
    [SerializeField] public float Time;
    [SerializeField] public GameObject TaskObject;
    //  [SerializeField] public int OrderIndex;

    public Task(int _priority, bool _active, string _message, float _time, GameObject _taskObject)
    {
        Priority = _priority;
        active = _active;
        message = _message;
        Time = _time;
        TaskObject = _taskObject;

    }
}

public class TaskOrganizer : MonoBehaviour
{
    public static int Score;
    [SerializeField]  int ScoreGain, ScoreLoss;
    [SerializeField] float minspawnRate, maxspawnRate, incrementAmout, incrementTime;

    public Text[] text;
    public Text[] TimerText;
    public float[] timervalue;
    public Task[] RegisteredTasks;
    public List<Task> ActiveTasks;
    [SerializeField] Transform[] pos;
    [SerializeField] Transform OGPos;
    bool[] poscheck;
    public GameObject check,cross;


    public bool busy;
    // Start is called before the first frame update
    void Start()
    {
        poscheck = new bool[text.Length];
        StartCoroutine(Spawner());
        StartCoroutine(IncrementIncrease());
    }
    IEnumerator IncrementIncrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(incrementTime);
            minspawnRate -= incrementAmout / 2;
            maxspawnRate -= incrementAmout / 2;



        }
    }
    IEnumerator Spawner()
    {
        while(true)
        {
            Debug.Log("bf");
            yield return new WaitForSeconds(Random.Range(minspawnRate, maxspawnRate));
            Debug.Log("her");
            if(!busy)
            {
                AddTask();
            }
           


        }
    }


    public void RemoveTask(GameObject taskobj, bool failed)
    {
        busy = true;
        int num = 0;

       

        for (int i = 0; i < ActiveTasks.Count; i++)
        {
            if (ActiveTasks[i].TaskObject == taskobj)
            {
                num = i;

            }
        }



        IEnumerator RemoveCoroutine = RemoveNumerator(num,failed);
        StartCoroutine(RemoveCoroutine);
    }
    IEnumerator RemoveNumerator(int num,bool failed)
    {

        int textnum = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i].text == ActiveTasks[num].message)
            {
                textnum = i;
                i = text.Length;
            }
        }

        if (failed)
        {
            Score -= ScoreLoss;
        }
        else
        {
            int extraTime;
            int.TryParse(TimerText[textnum].text, out extraTime);
            Score += ScoreGain + extraTime;

        }

        if (failed)
        {
            text[textnum].color = Color.red;
            TimerText[textnum].color = Color.red;
            GameObject obj = Instantiate(cross, text[textnum].transform.position, Quaternion.identity);
            obj.transform.SetParent(GameObject.FindGameObjectWithTag("UI").transform );
        }
        else
        {
            GameObject obj = Instantiate(check, text[textnum].transform.position, Quaternion.identity);
            obj.transform.SetParent(GameObject.FindGameObjectWithTag("UI").transform);
            text[textnum].color = Color.green;
            TimerText[textnum].color = Color.green;
        }
        yield return new WaitForSeconds(1f);

        ActiveTasks[num].TaskObject.SetActive(false);
        ActiveTasks.RemoveAt(num);
        Vector2 targetpos = new Vector2(OGPos.position.x, text[textnum].transform.position.y);
        while (Vector2.Distance(text[textnum].transform.position, targetpos) > 1f)
        {
            text[textnum].transform.position = Vector2.Lerp(text[textnum].transform.position, targetpos, 15 * Time.deltaTime);

            yield return null;
        }
        text[textnum].text = "";
        text[textnum].transform.position = OGPos.position;
        text[textnum].color = Color.white;
        TimerText[textnum].color = Color.white;

      //  StopAllCoroutines();

        IEnumerator SortingCoroutine = SortingNumerator;
       
        StartCoroutine(SortingCoroutine);


    }



    IEnumerator SortingNumerator
    {
      
        get
        {
            yield return new WaitForSeconds(.0001f);
            if(ActiveTasks.Count == 0)
            {
                busy = false;
                yield break;
            }

            bool sorting = true;
            while (sorting)
            {
                sorting = false;

                yield return null;
                for (int i = 0; i < ActiveTasks.Count - 1; i++)
                {
                    if (ActiveTasks[i].Priority > ActiveTasks[i + 1].Priority)
                    {

                        Task temp;
                        temp = ActiveTasks[i];
                        ActiveTasks[i] = ActiveTasks[i + 1];
                        ActiveTasks[i + 1] = temp;

                        sorting = true;
                    }

                }

            }






            for (int b = 0; b < poscheck.Length; b++)
            {
                poscheck[b] = false;
            }

            if (ActiveTasks.Count == 1)
            {
                
                for (int a = 0; a < text.Length; a++)
                {
                    if (text[a].text != "")
                    {
                        while ( Vector2.Distance(text[a].transform.position, pos[0].position) > .5f)
                        {
                            yield return null;
                            text[a].transform.position = Vector2.Lerp(text[a].transform.position, pos[0].position, 15 * Time.deltaTime);

                        }
                    }
                   

                }
                busy = false;
                yield break;
            }
            
            

            int finished = 0;
            while (finished < ActiveTasks.Count)
            {
               

                for (int i = 0; i < ActiveTasks.Count; i++)
                {
                    for (int a = 0; a < text.Length; a++)
                    {
                        

                        if (ActiveTasks.Count > 0 && text[a].text == ActiveTasks[i].message)
                        {
                            text[a].transform.position = Vector2.Lerp(text[a].transform.position, pos[i].position, 25 * Time.deltaTime);
                            yield return null;
                            if (Vector2.Distance(text[a].transform.position, pos[i].position) < .4f && !poscheck[i])
                            {
                                finished++;
                                poscheck[i] = true;

                            }
                            a = ActiveTasks.Count;
                        }

                    }

                }
               

                yield return null;
            }
            busy = false;
        }
    }

    public void AddTask()
    {
     
        if (ActiveTasks.Count >= 3 || busy)
        {
            return;
        }
        busy = true;
        bool taskfound = true;
        int random = 0;

        for(int i=0;i<15;i++)
        {

            random = Random.Range(0, RegisteredTasks.Length);
            if(RegisteredTasks[random].active)
            {
                i = 16;
            }
        }

        if (!RegisteredTasks[random].active)
        {
            busy = false;
            return;
        }

        if (ActiveTasks.Count > 0)
        {
            for (int i = 0; i < ActiveTasks.Count; i++)
            {

                if (RegisteredTasks[random].message == ActiveTasks[i].message)
                {

                    taskfound = false;
                    i = 4;
                }
            }

            if (!taskfound || !RegisteredTasks[random].active)
            {
                busy=false;
                AddTask();
                return;

            }

        }

       

        RegisteredTasks[random].TaskObject.SetActive(true);
        ActiveTasks.Add(new Task(RegisteredTasks[random].Priority, RegisteredTasks[random].active, RegisteredTasks[random].message, RegisteredTasks[random].Time, RegisteredTasks[random].TaskObject));
       


        IEnumerator AddCoroutine = AddNumerator(random);
        StartCoroutine(AddCoroutine);



    }


    IEnumerator AddNumerator(int indexx)
    {
       
        int currentText = 0;

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i].text == "")
            {
                text[i].text = RegisteredTasks[indexx].message;
                TimerText[i].text = RegisteredTasks[indexx].Time.ToString();
                timervalue[i] = RegisteredTasks[indexx].Time;
                currentText = i;
                i = text.Length;
                text[currentText].transform.position = OGPos.position;
            }
        }

      
        while (Vector2.Distance(text[currentText].transform.position, pos[pos.Length - 1].position) > .35f)
        {
            text[currentText].transform.position = Vector2.Lerp(text[currentText].transform.position, pos[pos.Length - 1].position, 10 * Time.deltaTime);

            yield return null;
        }

        yield return null;

        IEnumerator SortCoroutine = SortingNumerator;
        StartCoroutine(SortCoroutine);

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            

            AddTask();
        }
     

        for(int i=0;i<text.Length;i++)
        {
            if (text[i].text != "" )
            {

                if(!busy)
                {
                    timervalue[i] -= Time.deltaTime;
                }
            
                if(timervalue[i] < 999)
                {
                    if (timervalue[i] > 0)
                    {
                        if(timervalue[i] < 10)
                        {
                            TimerText[i].text = "0" + timervalue[i].ToString("F2");
                        }
                        else
                        {
                            TimerText[i].text = timervalue[i].ToString("F2");
                        }


                        TimerText[i].text = TimerText[i].text.Replace(".", ":");
                    }
                    else
                    {
                       
                        timervalue[i] = 1200;
                        TimerText[i].text = "00:00";
                        for(int a=0;a<ActiveTasks.Count;a++)
                        {
                            if(text[i].text == ActiveTasks[a].message)
                            {
                                RemoveTask(ActiveTasks[a].TaskObject,true);
                                a = ActiveTasks.Count;
                            }
                        }

                       
                    }
                }
                
             

            }
        }
       


    }
}
