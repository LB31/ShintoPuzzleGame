using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    public void Play()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void Exit()
    {
        // TODO save game in the future
        Application.Quit();
    }

    public void Controls()
    {

    }

    public void About()
    {

    }

}
