using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredState<T> : FSMState<T>
{
    FSM<T> _fsm;
    NPC _npc;
    T _idle;
    float _time;
    List<GameObject> _enemies;
    float _timePrediction = 0;

    public ScaredState(FSM<T> fsm, NPC npc, T idle)
    {
        _fsm = fsm;
        _npc = npc;
        _idle = idle;
        _enemies = new List<GameObject>();
    }

    public override void Execute()
    {
        // verifica si tengo enemigos cerca para escapar de ellos
        if (_time >= 0.2f)
        {
            Vector3 dir = Vector3.zero;
            Vector3 posPrediction;
            Collider[] targetsInViewRadius = Physics.OverlapSphere(_npc.transform.position, 5, LayerMask.GetMask("Player"));
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                GameObject target = targetsInViewRadius[i].gameObject;
                if (target.GetComponent<NPC>().Team != _npc.Team)
                {
                    Vector3 dirToTarget = _npc.transform.position - target.transform.position;
                    float distToTarget = Vector3.Distance(_npc.transform.position, target.transform.position);
                    if (!Physics.Raycast(_npc.transform.position, dirToTarget, distToTarget, LayerMask.GetMask("Wall")))
                    {
                        _enemies.Add(target);
                    }
                }
            }
            if (_enemies.Count > 0)
            {
                foreach (GameObject go in _enemies)
                {
                    posPrediction = go.transform.position + _timePrediction * go.GetComponent<NPC>().Rb.velocity.magnitude * go.transform.forward;
                    dir += _npc.transform.position - posPrediction;
                }
                _npc.Move(dir.normalized);
            }
            _time = 0;
        }
        else 
        {
            _time += Time.deltaTime;
            _enemies.Clear();
        }
    }

    public override void Awake()
    {
        _time = 0.2f;
    }

    public override void Sleep()
    {

    }
}
