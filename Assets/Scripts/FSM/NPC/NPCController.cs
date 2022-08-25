using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    FSM<string> _fsm;
    NPC _npc;

    void Awake()
    {
        // crea cada estado y sus transiciones
        _npc = GetComponent<NPC>();
        _fsm = new FSM<string>();
        IdleState<string> _idle = new IdleState<string>(_fsm, _npc, "patrol", "combat");
        PatrolState<string> _patrol = new PatrolState<string>(_fsm, _npc, "idle", "combat");
        CombatState<string> _combat = new CombatState<string>(_fsm, _npc, "idle");
        ScaredState<string> _scared = new ScaredState<string>(_fsm, _npc, "idle");
        DeadState<string> _dead = new DeadState<string>(_fsm, _npc);

        _idle.AddTransition("patrol", _patrol);
        _idle.AddTransition("combat", _combat);
        _idle.AddTransition("scared", _scared);
        _idle.AddTransition("dead", _dead);

        _patrol.AddTransition("idle", _idle);
        _patrol.AddTransition("combat", _combat);
        _patrol.AddTransition("scared", _scared);
        _patrol.AddTransition("dead", _dead);

        _combat.AddTransition("idle", _idle);
        _combat.AddTransition("patrol", _patrol);
        _combat.AddTransition("scared", _scared);
        _combat.AddTransition("dead", _dead);

        _scared.AddTransition("idle", _idle);
        _scared.AddTransition("patrol", _patrol);
        _scared.AddTransition("combat", _combat);
        _scared.AddTransition("dead", _dead);

        _fsm.SetInit(_idle);
    }

    private void Start()
    {

    }

    void Update()
    {
        //verifica si estoy muerto
        if (_npc.CurrentHealth <= 0)
        {
            _fsm.Transition("dead");
        }
        //verifica si estoy asustado
        else if (_npc.CurrentHealth < 20 && _npc.IsAlive)
        {
            _fsm.Transition("scared");
        }
        
        _fsm.OnUpdate();
    }
}
