using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState<T> : FSMState<T>
{
    FSM<T> _fsm;
    NPC _npc;
    T _patrol;
    T _combat;
    float time;
    
    public IdleState(FSM<T> fsm,NPC npc, T patrol, T combat)
    {
        _fsm = fsm;
        _npc = npc;
        _patrol = patrol;
        _combat = combat;
    }
    public override void Execute()
    {
        // verifica el tiempo que lleva en idle antes de volver a patrullar
        if (time < 2) 
        {
            time += Time.deltaTime;
            // verifica se ve enemigos para pasar a combate
            if (_npc.VisibleTargets != null)
            {
                if (_npc.VisibleTargets.Count > 0)
                {
                    _fsm.Transition(_combat);
                }
            }
        }
        else
        {
            _fsm.Transition(_patrol);
        }
    }
    public override void Awake()
    {
        time = 0;
        _npc.Rb.velocity = Vector3.zero;
    }
    public override void Sleep()
    {

    }
}