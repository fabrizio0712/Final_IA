using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState<T> : FSMState<T>
{
    FSM<T> _fsm;
    List<Node> _list;
    NPC _npc;
    T _idle;
    T _combat;
    int _nextPoint = 0;

    public PatrolState(FSM<T> fsm, NPC npc, T idle, T combat)
    {
        _fsm = fsm;
        _npc = npc;
        _idle = idle;
        _combat = combat;
    }
    public override void Awake()
    {
        // consigue lista de nodos para moverse
        _nextPoint = 0;
        _list =_npc.Agent.PathFindingAStar(_npc.CurrentNode, _npc.Gm.GetRandomNode());
    }
    public override void Execute()
    {
        Run();
    }
    public void Run()
    {
        // verifica si ve enemigos para pasar a combate
        if (_npc.VisibleTargets != null)
        {
            if (_npc.VisibleTargets.Count > 0)
            {
                _fsm.Transition(_combat);
            }
        }
        // verifica la lista de nodos para saber a donde moverse, si llego a destino pasa a idle
        var point = _list[_nextPoint];
        var posPoint = point.transform.position;
        posPoint.y = _npc.gameObject.transform.position.y;
        Vector3 dir = posPoint - _npc.gameObject.transform.position;
        if (dir.magnitude < 0.8)
        {
            if (_nextPoint + 1 < _list.Count)
                _nextPoint++;
            else
            {
                _fsm.Transition(_idle);
            }
        }
        
        _npc.Move(dir.normalized);
    }
    public override void Sleep()
    {
        _npc.Move(Vector3.zero);
    }
}
