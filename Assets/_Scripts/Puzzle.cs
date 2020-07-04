using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private Kami currentKami;
    [SerializeField]
    private GameObject puzzleUi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPuzzle(GameManager.KamiType selectedKami)
    {
        foreach (Kami kami in GameManager.gameManager.kamis)
        {
            if (kami.kamiName == selectedKami.ToString())
            {
                currentKami = kami;

                BuildPuzzle();
            }
        }
    }

    public void BuildPuzzle()
    {
        var puzzleBox = puzzleUi.transform.Find("Background_Panel/Puzzle_Text");
        var textComponent = puzzleBox.GetComponent<TMP_Text>();
        textComponent.text = currentKami.puzzleText;
    }

    public void CheckAnswer()
    {
        var puzzleBox = puzzleUi.transform.Find("Background_Panel/Puzzle_Text");
       
        string correctAnswer = currentKami.puzzleAnswer;
        string answerGiven = puzzleBox.GetComponentInChildren<TMP_InputField>().text;
        if (answerGiven == correctAnswer)
        {
            Debug.Log("You are correct");
            PlayMakerFSM.BroadcastEvent("puzzleFinished");
        }
        else
        {
            Debug.Log("You are incorrect");
        }
    }
    public void StopPuzzle()
    {
        PlayMakerFSM.BroadcastEvent("cancelInteraction");
    }
}
