using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] float _viewRadius;
    [Range (0,360)]
    [SerializeField] float _viewAngle;
    [SerializeField] LayerMask _targetMask;
    [SerializeField] LayerMask _obstacleMask;
    private List<GameObject> _visibleTargets;
    private NPC _owner;

    public NPC Owner { get => _owner; set => _owner = value; }
    public float ViewRadius { get => _viewRadius; set => _viewRadius = value; }
    public float ViewAngle { get => _viewAngle; set => _viewAngle = value; }

    void Start()
    {
        _visibleTargets = new List<GameObject>();
    }
    
    public List<GameObject> FindVisibleTargets() 
    {
        _visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, _targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++) 
        {
            GameObject target = targetsInViewRadius[i].gameObject;
            if (target.GetComponent<NPC>().Team != _owner.Team && target.GetComponent<NPC>().IsAlive)
            {
                Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < _viewAngle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.transform.position);
                    if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, _obstacleMask))
                    {
                        _visibleTargets.Add(target.gameObject);
                    }
                }
            }
        }
        return _visibleTargets;
    }
    public Vector3 DirFromAngle(float AnglesInDegrees, bool AngleIsGlobal) 
    {
        if (!AngleIsGlobal) 
        {
            AnglesInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(AnglesInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(AnglesInDegrees * Mathf.Deg2Rad));
    }
}
