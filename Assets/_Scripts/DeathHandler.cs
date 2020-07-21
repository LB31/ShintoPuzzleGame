using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject spawn;
    public GameObject puzzleManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            cc.enabled = false;
            player.transform.position = spawn.transform.position;
            cc.enabled = true;
            puzzleManager.GetComponent<PuzzleManagerSusanno>().PuzzleStart();
        }
    }

}
