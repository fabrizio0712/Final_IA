using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState<T> : FSMState<T>
{
    FSM<T> _fsm;
    T _idle;
    float _time;
    float _timePrediction;
    List<GameObject> _visibleTargets;
    NPC _npc;
    GameObject _target;
    Vector3 _targetLastPos;
    float _attackSpeed = 1;
    float _attackTimer = 0;
    bool _hit;

    public List<GameObject> VisibleTargets { get => _visibleTargets; set => _visibleTargets = value; }

    public CombatState(FSM<T> fsm, NPC npc, T idle)
    {
        _fsm = fsm;
        _idle = idle;
        _npc = npc;
        _timePrediction = _npc.TimePrediction;
        _time = 1;
    }
    public override void Execute()
    {
        if (_time < 0.2f)
        {
            _time += Time.deltaTime;
        }
        else
        {
            _visibleTargets = _npc.Fov.FindVisibleTargets();
            _time = 0;
        }
        if (_visibleTargets != null)
        {
            if (_visibleTargets.Count > 0)
            {
                Vector3 distance = new Vector3(3000, 0, 0);
                foreach (GameObject go in _visibleTargets)
                {
                    if (go != null)
                    {
                        Vector3 temp = go.transform.position - _npc.transform.position;
                        if (temp.magnitude < distance.magnitude)
                        {
                            distance = temp;
                            _target = go;
                        }
                    }
                }
                if (_time < 0.2f)
                {
                    if(_target != null) _targetLastPos = _target.transform.position;
                }
                Vector3 dist = _targetLastPos - _npc.transform.position;
                if (dist.magnitude > 1f)
                {
                    _npc.AnimController.Anim.SetBool("IsPunching", false);
                    // Pursuit
                    Vector3 posPrediction = _target.transform.position + _target.transform.forward * _npc.Rb.velocity.magnitude * _timePrediction;
                    Vector3 dir = (posPrediction - _npc.transform.position).normalized;
                    _npc.Move(dir);
                }
                else
                {
                    _npc.transform.forward = distance.normalized;
                    _npc.Rb.velocity = Vector3.zero;
                    _npc.AnimController.Anim.SetBool("IsPunching", true);
                    if (_attackTimer > 1 && !_hit)
                    {
                        _target.GetComponent<NPC>().GetDamage(10);
                        _hit = true;
                    }
                    else if (_attackTimer > 2.2f) 
                    {
                        _attackTimer = 0;
                        _hit = false;
                    }
                    else 
                    {
                        _attackTimer += Time.deltaTime;
                    }
                }
            }
            else
            {
                _npc.Rb.velocity = Vector3.zero;
                _fsm.Transition(_idle);
            }
        }
    }
    public override void Awake()
    {
        _time = 0.2f;
        _attackTimer = 0f;
        _hit = false;
    }
    public override void Sleep()
    {
        _npc.AnimController.Anim.SetBool("IsPunching", false);
    }
}
