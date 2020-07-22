using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{


    public void Play()
    {
        Time.timeScale = 1;
        TriggerMouseInteraction(true);
        gameObject.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
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
