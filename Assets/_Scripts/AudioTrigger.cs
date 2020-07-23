using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public string AudioName;
    private bool audioPlayed;

    private void OnTriggerEnter(Collider other)
    {
        if (audioPlayed) return;
        AudioController.Instance.ChangeAudio(AudioName);
        audioPlayed = true;
    }
}
