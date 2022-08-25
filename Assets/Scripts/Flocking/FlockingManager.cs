using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    NPC _npc;
    public float radius;
    public LayerMask maskBoids;
    IFlockingBehavior[] _behaviours;
    private void Awake()
    {
        _npc = GetComponent<NPC>();
        _behaviours = GetComponents<IFlockingBehavior>();
    }
    public Vector3 GetFlokingDir()
    {
        Collider[] colls = Physics.OverlapSphere(_npc.transform.position, radius, maskBoids);
        List<FlockingManager> _boids = new List<FlockingManager>();
        for (int i = 0; i < colls.Length; i++)
        {
            var current = colls[i];
            if (current.gameObject == _npc.gameObject && _npc.Team == current.gameObject.GetComponent<NPC>().Team) continue;
            FlockingManager currentBoid = current.GetComponent<FlockingManager>();
            if (currentBoid == null) continue;
            _boids.Add(currentBoid);
        }
        Vector3 dir = Vector3.zero;
        for (int i = 0; i < _behaviours.Length; i++)
        {
            dir += _behaviours[i].GetDir(_boids, this);
        }
        return dir.normalized;
    }
    public NPC GetEntity
    {
        get
        {
            return _npc;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
