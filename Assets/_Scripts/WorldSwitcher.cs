using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldSwitcher : MonoBehaviour
{

    public static WorldSwitcher Instance;

    public Image SceneTransition;

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
        SecondScene.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(VisualizeSceneChange(true));
        }
    }

    private void SwitchWorld()
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

            TriggerPlayerControls(true);

            Player.GetComponent<FPSController>().WalkSpeed = 1.5f;

            SecondScene.GetComponent<DreamWorldController>().enabled = true;
        }
        else
        {
            // Activate flashlight again
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

    public IEnumerator VisualizeSceneChange(bool kami)
    {
        SceneTransition.enabled = true;
        float fadeDuration = 1;
        Color color = SceneTransition.color;
        for (float i = 0; i <= 1; i += Time.deltaTime * fadeDuration)
        {
            color.a = i;
            SceneTransition.color = color;
            yield return null;
        }

        if (kami)
            SwitchWorld();
        else
        {

        }

        for (float i = 1; i >= 0; i -= Time.deltaTime * fadeDuration)
        {
            color.a = i;
            SceneTransition.color = color;
            yield return null;
        }

        SceneTransition.enabled = false;
    }


}
