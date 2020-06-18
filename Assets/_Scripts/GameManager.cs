using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset json;

    public GameObject MobileUI;
    public GameObject ReticleUI;
    public FPSController fpsController;

    public enum KamiType
    {
        Ametarasu = 0,
        Susanno = 1
    }
    public KamiType selectedKamiType;

    public List<Kami> kamis;

    public static GameManager gameManager;

    //erstelen gamemanager als singleton
    private void Awake()
    {
        if (gameManager)
        {
            return;
        }
        gameManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ParseJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ParseJson()
    {
        Kamis kamisInJson = JsonUtility.FromJson<Kamis>(json.text);
        kamis = new List<Kami>();
        foreach (Kami kami in kamisInJson.kamis)
        {
            kamis.Add(kami);
        }
    }

    public void TriggerUi(bool isActive)
    {
        //MobileUI.SetActive(false);
        ReticleUI.SetActive(!isActive);
        fpsController.enabled = !isActive;
        if (isActive)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Cursor.visible = isActive;
    }
}
