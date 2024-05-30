using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AudioSource sound1;
    public AudioSource sound2;

    public void Start()
    {
        sound1.Stop();
        sound2.Play();

    }
    public void Update()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    //main menu logika sve je za on click funckiju
    public void GoToScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void QuitApp()
    {
        Application.Quit();
    }
    public void RestartScene()
    {
        StartCoroutine(ReloadScene());
        
    }
    //ovo je kad se reloaduje da loaduje opet sve i aktivira svaki awake koj je bioo
    IEnumerator ReloadScene()
    {
        

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        //cim zavrsi stavlja trenutnu scenu
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneManager.GetActiveScene().name));

    }
}
