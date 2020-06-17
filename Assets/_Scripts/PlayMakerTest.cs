using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMakerTest : MonoBehaviour
{
    public PlayMakerFSM FSM;
    public string EventName;
    

    private void OnMouseDown()
    {
        //FSM.Fsm.Event(EventName);
        //PlayMakerFSM.BroadcastEvent(EventName);
        FsmInt defeatedYokai = FSM.Fsm.Variables.GetFsmInt("defeatedYokai");
        defeatedYokai.Value = 5;
        print(defeatedYokai);
    }

    public void DoMonkeyStuff(Vector2 bubi)
    {
        Debug.Log("MONKEY ATTACKS");
    }

}
