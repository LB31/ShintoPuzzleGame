using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSwitcher : MonoBehaviour
{

    public static WorldSwitcher Instance;

    public enum SendingKami
    {
        Bob,
        Amaterasu,
        Susanoo,
        Inari,
    }

    public SendingKami sendingKami;


    public GameObject MainScene;
    public GameObject SecondScene;
    public Transform Player;
    public Transform SpawnPoint;

    private Vector3 lastPlayerPosition;
    private Quaternion lastPlayerRotation;
    private Quaternion lastCameraRotation;
    private Camera playerCam;

    private bool mainWorld = true;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playerCam = Player.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchWorld();
        }
    }

    public void SwitchWorld()
    {
        if (mainWorld)
        {
            // Remove flashlight for dream world
            playerCam.GetComponentInChildren<Light>().enabled = false;
            // Store previous Transform
            lastPlayerPosition = Player.position;
            lastPlayerRotation = Player.rotation;
            lastCameraRotation = playerCam.transform.localRotation;

            SecondScene.SetActive(mainWorld);
            MainScene.SetActive(!mainWorld);

            TriggerPlayerControls(false);

            Player.position = SpawnPoint.position;
            Player.rotation = SpawnPoint.rotation;
            playerCam.transform.localRotation = SpawnPoint.rotation;

            //await Task.Delay(2000);
            TriggerPlayerControls(true);

            Player.GetComponent<FPSController>().WalkSpeed = 1.5f;

            SecondScene.GetComponent<DreamWorldController>().enabled = true;
        }
        else
        {
            // Remove flashlight for dream world
            playerCam.GetComponentInChildren<Light>().enabled = true;

            SecondScene.SetActive(mainWorld);
            MainScene.SetActive(!mainWorld);

            TriggerPlayerControls(false);

            Player.position = lastPlayerPosition;
            Player.rotation = lastPlayerRotation;
            playerCam.transform.localRotation = lastCameraRotation;

            TriggerPlayerControls(true);

            Player.GetComponent<FPSController>().WalkSpeed = 2;
        }

        mainWorld = !mainWorld;
    }

    private void TriggerPlayerControls(bool on)
    {
        Player.GetComponent<CharacterController>().enabled = on;
        Player.GetComponent<FPSController>().enabled = on;
    }


}
