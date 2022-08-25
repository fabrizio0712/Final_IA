using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceBehavior : MonoBehaviour
{
    private NPC _npc;
    private Vector3 _dir;
    private void Start()
    {
        _npc = gameObject.GetComponent<NPC>();
    }
    public Vector3 GetDir()
    {
        //Obstacle Avoidance
        Collider[] obstacles = Physics.OverlapSphere(transform.position, _npc.Radius, _npc.ObstacleMask);
        Transform obsSave = null;
        _dir = Vector3.zero;
        var count = obstacles.Length;

        for (int i = 0; i < count; i++)
        {
            var currObs = obstacles[i].transform;
            if (obsSave == null)
            {
                obsSave = currObs;
            }
            else if (Vector3.Distance(transform.position, obsSave.position) > Vector3.Distance(transform.position, currObs.position))
            {
                obsSave = currObs;
            }
        }
        if (obsSave != null)
        {
            Vector3 dirObsToNpc = (transform.position - obsSave.position).normalized * _npc.AvoidWeight;
            _dir += dirObsToNpc;
        }
        return _dir;
    }
}
