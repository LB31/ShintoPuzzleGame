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
        foreach(Kami kami in GameManager.Instance.kamis)
        {
            if (kami.kamiName == selectedKami.ToString())
            {
                dialogSequence = 0;
                currentKami = kami;
                BuildDialog();
            }
        }
    }

    private void BuildDialog()
    {
        string[] dialogs = currentKami.dialogs;

        var dialogBox = dialogUI.transform.Find("Panel/Dialog");
        var textComponent = dialogBox.GetComponent<TMP_Text>();
        if(dialogs.Length > dialogSequence)
        {
            textComponent.text = dialogs[dialogSequence];
        }
        else
        {
            PlayMakerFSM.BroadcastEvent("dialogFinished");
        }
        
    }
    
    public void DialogSequence()
    {
        dialogSequence++;
        BuildDialog();
    }

}
