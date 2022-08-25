using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
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
        BossCombatState<string> _combat = new BossCombatState<string>(_fsm, _npc, "idle", "casting");
        CastingState<string> _casting = new CastingState<string>(_fsm, _npc, "idle");
        DeadState<string> _dead = new DeadState<string>(_fsm, _npc);

        _idle.AddTransition("patrol", _patrol);
        _idle.AddTransition("combat", _combat);
        _idle.AddTransition("dead", _dead);

        _patrol.AddTransition("idle", _idle);
        _patrol.AddTransition("combat", _combat);
        _patrol.AddTransition("dead", _dead);

        _combat.AddTransition("idle", _idle);
        _combat.AddTransition("patrol", _patrol);
        _combat.AddTransition("casting", _casting);
        _combat.AddTransition("dead", _dead);

        _casting.AddTransition("idle", _idle);
        _casting.AddTransition("dead", _dead);

        _fsm.SetInit(_idle);
    }

    private void Start()
    {
       
    }

    void Update()
    {
        // verifica si estoy muerto
        if (_npc.CurrentHealth <= 0)
        {
            _fsm.Transition("dead");
        }
        else
        {
            // verifica el cooldown para usar habilidades
            if (_npc.AbilityTimer < _npc.AbilityCooldown)
            {
                _npc.AbilityTimer += Time.deltaTime;
            }
        }
        _fsm.OnUpdate();
    }
}
