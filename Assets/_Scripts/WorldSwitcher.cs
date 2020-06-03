using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSwitcher : MonoBehaviour
{

    public static WorldSwitcher Instance;

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
            mainWorld = !mainWorld;

            lastPlayerPosition = Player.position;
            lastPlayerRotation = Player.rotation;
            lastCameraRotation = playerCam.transform.localRotation;

            print(lastPlayerRotation + " lastPlayerRotation");
            print(lastCameraRotation + "lastCameraRotation");

            SecondScene.SetActive(true);
            MainScene.SetActive(false);

            Player.GetComponent<CharacterController>().enabled = false;

            Player.position = SpawnPoint.position;
            Player.rotation = SpawnPoint.rotation;
            playerCam.transform.localRotation = SpawnPoint.rotation;

            //Player.GetComponent<CharacterController>().enabled = true;

        }
        else
        {
            mainWorld = !mainWorld;

            SecondScene.SetActive(false);
            MainScene.SetActive(true);

            Player.GetComponent<CharacterController>().enabled = false;

            Player.position = lastPlayerPosition;
            Player.rotation = lastPlayerRotation;
            playerCam.transform.localRotation = lastCameraRotation;

            print(Player.rotation);
            print(playerCam.transform.localRotation);


            //Player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
