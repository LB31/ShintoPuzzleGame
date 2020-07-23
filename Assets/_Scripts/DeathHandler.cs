using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public WorldSwitcher ws;
    public GameObject spawn;
    public GameObject puzzleManager;
    public Transform player;

    private async void OnTriggerEnter(Collider other)
    {
        ws.TriggerPlayerControls(false);
        StartCoroutine(ws.VisualizeSceneChange(false));
        await Task.Delay(1000);
        player.position = spawn.transform.position;
        ws.TriggerPlayerControls(true);
        puzzleManager.GetComponent<PuzzleManagerSusanno>().PuzzleStart();
    }

}
