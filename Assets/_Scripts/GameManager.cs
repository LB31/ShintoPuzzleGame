using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private TextAsset json;

    public GameObject MobileUI;
    public GameObject ReticleUI;
    public FPSController fpsController;
    public bool Mobile;

    public enum KamiType
    {
        Ametarasu = 0,
        Susanno = 1,
        Amabie = 2,
        Futakuchi_onna = 3,
    }
    //public KamiType selectedKamiType;

    public List<Kami> kamis;

    

    //erstelen gamemanager als singleton
    private void Awake()
    {
        if (Instance)
        {
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ParseJson();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
            print("exit");
        }
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
        CharacterController cc = FindObjectOfType<CharacterController>();
        if (Mobile)
        {
            MobileUI.SetActive(!isActive);
            fpsController.enabled = !isActive;
            cc.enabled = !isActive;
            return;
        }

        ReticleUI.SetActive(!isActive);
        fpsController.enabled = !isActive;
        cc.enabled = !isActive;
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

    public void ExitGame()
    {
        Application.Quit();
    }
}
