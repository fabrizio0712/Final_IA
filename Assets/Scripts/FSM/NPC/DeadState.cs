using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState<T> : FSMState<T>
{
    FSM<T> _fsm;
    NPC _npc;
    T _patrol;
    T _combat;
    T _scared;
    float time;
    public DeadState(FSM<T> fsm, NPC npc)
    {
        _fsm = fsm;
        _npc = npc;
    }
    public override void Execute()
    {

    }
    public override void Awake()
    {
        _npc.IsAlive = false;
        _npc.AnimController.Anim.SetBool("IsAlive", false);
        _npc.Rb.isKinematic = true;
        _npc.Collider.isTrigger = true;
    }
    public override void Sleep()
    {

    }
}