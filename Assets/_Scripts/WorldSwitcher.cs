using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSwitcher : MonoBehaviour
{
    public GameObject MainScene;
    public GameObject SecondScene;
    public Transform Player;
    public Transform SpawnPoint;

    private Vector3 lastPlayerPos;
    private Quaternion lastPlayerRotation;

    private bool mainWorld = true;

    //private void Start()
    //{
    //    DontDestroyOnLoad(this);
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchWorld();
        }
    }

    void SwitchWorld()
    {
        if (mainWorld)
        {

            mainWorld = !mainWorld;
            lastPlayerPos = Player.position;
            lastPlayerRotation = Player.rotation;
            SecondScene.SetActive(true);
            MainScene.SetActive(false);
            Player.GetComponent<CharacterController>().enabled = false;
            Player.position = SpawnPoint.position;
            Player.rotation = SpawnPoint.rotation;
            Player.GetComponent<CharacterController>().enabled = true;

        }
        else
        {
            mainWorld = !mainWorld;
            SecondScene.SetActive(false);
            MainScene.SetActive(true);
            Player.GetComponent<CharacterController>().enabled = false;
            Player.position = lastPlayerPos;
            Player.rotation = lastPlayerRotation;
            Player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
