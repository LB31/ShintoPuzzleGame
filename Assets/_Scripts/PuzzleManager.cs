using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private enum GodTypes
    {
        Ametarasu = 0,
        Susanno = 1
    }
    [SerializeField]
    private TextAsset json;
    [SerializeField]
    private GodTypes selectedGodType;
    [SerializeField]
    private GameObject puzzleTextField;
    private God selectedGod;
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private GameObject canvasPuzzle;
    [SerializeField]
    private GameObject canvasDialog;
    [SerializeField]
    private float timeLapse;

    private float timeCounter = 0;

    private int currentCharacterIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PuzzleUI();
    }

    private void PuzzleUI()
    {
        Gods godsInJson = JsonUtility.FromJson<Gods>(json.text);
        int selectedGodId = (int)selectedGodType;
        foreach(God god in godsInJson.gods)
        {
            if(selectedGodId == god.id)
            {
                selectedGod = god;
                puzzleTextField.GetComponent<TMP_Text>().text = god.puzzleText;
            }
        }
    }

    public void CheckAnswer()
    {
        string correctAnswer = selectedGod.puzzleAnswer;
        string answerGiven = puzzleTextField.GetComponentInChildren<TMP_InputField>().text;
        if(answerGiven == correctAnswer)
        {
            Debug.Log("You are correct");
            canvasPuzzle.SetActive(false);
            playerCamera.SetActive(true);
        }
        else
        {
            Debug.Log("You are incorrect");
        }
    }

    public void StartDialog()
    {
        string[] dialogs = selectedGod.dialogs;
        
        foreach (string dialog in dialogs)
        {
            if (isTyping(dialog))
            {
                timeCounter += Time.deltaTime;
                if(timeCounter >= timeLapse)
                {
                    ++currentCharacterIndex;
                    canvasPuzzle.transform.Find("Dialog").GetComponent<TMP_Text>().text = dialog.Substring(0, currentCharacterIndex);
                    timeCounter = 0f;
                }
            }
        }
    }

    private bool isTyping(string dialog)
    {
        return currentCharacterIndex < dialog.Length;
    }
}
