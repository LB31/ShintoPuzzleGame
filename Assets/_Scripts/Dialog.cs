using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using TMPro;
using System;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogUI;
    [SerializeField]
    private float dialogSpeed;
    private PlayMakerFSM fsm;

    private Kami currentKami;

    public int dialogSequence;
    private IEnumerator currentCoroutine;
    //public bool isPuzzleFinished = false;


    public void StartDialog(GameManager.KamiType selectedKami, PlayMakerFSM sendingFSM)
    {
        fsm = sendingFSM;
        foreach (Kami kami in GameManager.Instance.kamis)
        {
            if (kami.kamiName == selectedKami.ToString())
            {
              
                dialogSequence = 0;
                currentKami = kami;
                GetDialogName();
                dialogUI.transform.Find("DialogPanel/YesButton").gameObject.SetActive(false);
                dialogUI.transform.Find("DialogPanel/NextButton").gameObject.SetActive(true);

                currentCoroutine = BuildDialog();
                StartCoroutine(currentCoroutine);
            }
        }
    }

    private IEnumerator BuildDialog()
    {
        string[] dialogs;
        Debug.Log(fsm.FsmVariables.FindFsmBool("isPuzzleFinished").Value);
        if (fsm.FsmVariables.FindFsmBool("isPuzzleFinished").Value)
        {
            dialogs = currentKami.dialogsAfterPuzzle;
        }
        else
        {
            dialogs = currentKami.dialogs;
        }

        var dialogBox = dialogUI.transform.Find("DialogPanel/Dialog");
        var textComponent = dialogBox.GetComponent<TMP_Text>();

        if (dialogSequence < dialogs.Length)
        {
            if(dialogSequence + 1 == dialogs.Length)
            {
                dialogUI.transform.Find("DialogPanel/NextButton").gameObject.SetActive(false);
                dialogUI.transform.Find("DialogPanel/YesButton").gameObject.SetActive(true);
            }
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
