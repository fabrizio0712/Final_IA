using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentBehavior : MonoBehaviour, IFlockingBehavior
{
    public float multiplier = 1;
    public Vector3 GetDir(List<FlockingManager> boids, FlockingManager entity)
    {
        Vector3 dir = Vector3.zero;
        for (int i = 0; i < boids.Count; i++)
        {
            dir += boids[i].GetEntity.transform.forward;
        }
        if (boids.Count != 0)
            dir /= boids.Count;

        return dir * multiplier;
    }
}
