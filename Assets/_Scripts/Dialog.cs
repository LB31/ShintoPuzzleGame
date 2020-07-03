﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using TMPro;
using System;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogUI;
    [SerializeField]
    private float dialogSpeed;
    [SerializeField]
    private PlayMakerFSM fsm;


    private Kami currentKami;

    public int dialogSequence;
    private IEnumerator currentCoroutine;
    //public bool isPuzzleFinished = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDialog(GameManager.KamiType selectedKami)
    {
        foreach (Kami kami in GameManager.gameManager.kamis)
        {
            if (kami.kamiName == selectedKami.ToString())
            {
              
                dialogSequence = 0;
                currentKami = kami;
                GetDialogName();

                currentCoroutine = BuildDialog();
                StartCoroutine(currentCoroutine);
            }
        }
    }

    private IEnumerator BuildDialog()
    {
        string[] dialogs = currentKami.dialogs;
        if (fsm.FsmVariables.FindFsmBool("isPuzzleFinished").Value)
        {
            dialogs = currentKami.dialogsAfterPuzzle;
        }

        var dialogBox = dialogUI.transform.Find("DialogPanel/Dialog");
        var textComponent = dialogBox.GetComponent<TMP_Text>();

        if (dialogSequence < dialogs.Length)
        {
            textComponent.text = "";
            foreach (char letter in dialogs[dialogSequence])
            {
                textComponent.text += letter;
                yield return new WaitForSeconds(dialogSpeed);
            }

        }
        else
        {
            PlayMakerFSM.BroadcastEvent("dialogFinished");
        }

    }

    private void GetDialogName()
    {
        var dialogBox = dialogUI.transform.Find("NamePanel/Name");
        var textComponent = dialogBox.GetComponent<TMP_Text>();

        textComponent.text = currentKami.kamiName;
    }

    public void DialogSequence()
    {
        StopCoroutine(currentCoroutine);
        dialogSequence++; 
        currentCoroutine = BuildDialog();
        StartCoroutine(currentCoroutine);
    }

    public void StopDialog()
    {
        StopCoroutine(currentCoroutine);
        PlayMakerFSM.BroadcastEvent("cancelInteraction");
    }
}
