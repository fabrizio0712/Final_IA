using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CohesionBehavior : MonoBehaviour, IFlockingBehavior
{
    public float multiplier = 1;
    public Vector3 GetDir(List<FlockingManager> boids, FlockingManager entity)
    {
        if (boids.Count == 0) return Vector3.zero;
        Vector3 center = Vector3.zero;
        for (int i = 0; i < boids.Count; i++)
        {
            center = boids[i].GetEntity.transform.position;
        }
        center /= boids.Count;
        Vector3 dir = (center - entity.GetEntity.transform.position).normalized;
        return dir * multiplier;
    }
}
