using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject MobileUI;

    // 0 = PC, 1 = Mobile
    public GameObject[] Controls;

    private void Start()
    {
        Pause();
        if (GameManager.Instance.Mobile)
        {
            Controls[1].SetActive(true);
        }
        else
        {
            Controls[0].SetActive(true);
        }
    }

    public void Play()
    {
        Time.timeScale = 1;
        if (GameManager.Instance.Mobile)
            MobileUI.SetActive(true);
        TriggerMouseInteraction(true);
        gameObject.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        if (GameManager.Instance.Mobile)
            MobileUI.SetActive(false);
        TriggerMouseInteraction(false);
        gameObject.SetActive(true);
    }

    public void Exit()
    {
        // TODO save game in the future
        Application.Quit();
    }


    public void Back()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        // activate main menu again
        transform.GetChild(1).gameObject.SetActive(true);
    }

    // true: lock, false: unlock
    private void TriggerMouseInteraction(bool trigger)
    {
        if (GameManager.Instance.Mobile) return;
        Cursor.lockState = trigger ? CursorLockMode.Locked : CursorLockMode.None;
    }

}
