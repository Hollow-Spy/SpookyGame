using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadinganim;

    public void LoadScene(int id)
    {
        loadinganim.SetActive(true);
        StartCoroutine(LoadingScene(id));
    }

    IEnumerator LoadingScene(int id)
    {
        yield return new WaitForSeconds(1.5f);
        AsyncOperation loadingprogress = SceneManager.LoadSceneAsync(id);
        while (!loadingprogress.isDone)
        {
            yield return null;
        }

    }

}

