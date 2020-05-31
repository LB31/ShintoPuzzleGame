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
    private GodTypes selectedGod;
    [SerializeField]
    private GameObject puzzleTextField;
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
        int selectedGodId = (int)selectedGod;
        foreach(God god in godsInJson.gods)
        {
            if(selectedGodId == god.id)
            {
                puzzleTextField.GetComponent<TMP_Text>().text = god.puzzleText;
            }
        }

    }
}
