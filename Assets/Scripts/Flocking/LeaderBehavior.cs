using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBehavior : MonoBehaviour, IFlockingBehavior
{
    public float multiplier;
    public Vector3 GetDir(List<FlockingManager> boids, FlockingManager entity)
    {
        Vector3 targetDist = entity.gameObject.transform.position - entity.GetEntity.Leader.transform.position;
        if (targetDist.magnitude < 3)
        {
            Vector3 targetPos = entity.GetEntity.Leader.transform.position;
            return (targetPos - entity.GetEntity.transform.position).normalized * multiplier;
        }
        else return Vector3.zero;
    }
}
