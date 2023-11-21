using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoading : MonoBehaviour
{
    //[SerializeField]
    //private Image progressBarImage;

    //private void Start()
    //{
    //    StartCoroutine(LoadAsyncOperation());
    //}

    //IEnumerator LoadAsyncOperation()
    //{
    //    AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);
    //    while (gameLevel.progress < 1) 
    //    {
    //        progressBarImage.fillAmount = gameLevel.progress;
    //        yield return new WaitForEndOfFrame();
    //    }

    //}    









    [SerializeField]
    private Slider progressBarSlider;

    private AsyncOperation operation;
    [SerializeField]
    private Canvas canvas;
    // Start is called before the first frame update
    private void Awake()
    {

        canvas.gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(string sceneName)
    {
        UpdateProgressUI(0);
        canvas.gameObject.SetActive(true);
        StartCoroutine(BeginSceneLoading(sceneName));
    }

    private IEnumerator BeginSceneLoading(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            UpdateProgressUI(operation.progress);
            yield return null;
        }
        UpdateProgressUI(operation.progress);
        operation = null;
        canvas.gameObject.SetActive(false);


    }

    private void UpdateProgressUI(float progress)
    {
        progressBarSlider.value = progress;
    }



}
