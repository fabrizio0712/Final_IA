using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingState<T> : FSMState<T>
{
    FSM<T> _fsm;
    NPC _npc;
    T _idle;
    float _time;
    int _amigo;
    int _enemigo;
    bool _casted;
    Roulette _roulette;
    Dictionary<GameObject, int> _dictionary;
    public CastingState(FSM<T> fsm, NPC npc, T idle)
    {
        _fsm = fsm;
        _npc = npc;
        _idle = idle;
        _roulette = new Roulette();
        _dictionary = new Dictionary<GameObject, int>();
        _casted = false;
    }
    public override void Execute()
    {
        if (_time > 2.28f && !_casted)
        {
            GameObject ability = GameObject.Instantiate(_roulette.Run(_dictionary),_npc.gameObject.transform.position,Quaternion.identity);
            ability.GetComponent<IAbility>().Initialized(_npc.Team);
            _casted = true;
        }
        else if(_time >= 4.05f)
        {
            _fsm.Transition(_idle);
        }
        else 
        {
            _time += Time.deltaTime;
        }
    }
    public override void Awake()
    {
        // armo el diccionario considerando la cantidad de aliados y enemigos cercanos
        _dictionary.Clear();
        _amigo = 0;
        _enemigo = 0;
        _time = 0;
        _npc.Rb.velocity = Vector3.zero;
        Collider[] obstacles = Physics.OverlapSphere(_npc.gameObject.transform.position, _npc.Radius, _npc.PlayerMask);
        foreach(Collider coll in obstacles) 
        {
            if (coll.gameObject.GetComponent<NPC>().Team == _npc.Team)
            {
                _amigo += 1;
            }
            else _enemigo += 1;
        }
        _dictionary.Add(_npc.HealingRift, _amigo);
        _dictionary.Add(_npc.AoE, _enemigo);
        _npc.AnimController.Anim.SetBool("IsCasting", true);
    }
    public override void Sleep()
    {
        _npc.AnimController.Anim.SetBool("IsCasting", false);
        _npc.AbilityTimer = 0;
        _casted = false;
    }
}
