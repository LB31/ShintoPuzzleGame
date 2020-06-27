using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using TMPro;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogUI;

    private Kami currentKami;

    public int dialogSequence;
    private IEnumerator currentCoroutine;

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
        var dialogBox = dialogUI.transform.Find("DialogPanel/Dialog");
        var textComponent = dialogBox.GetComponent<TMP_Text>();

        if (dialogSequence < dialogs.Length)
        {
            textComponent.text = "";
            foreach (char letter in dialogs[dialogSequence])
            {
                textComponent.text += letter;
                yield return new WaitForSeconds(0.05f);
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
}
