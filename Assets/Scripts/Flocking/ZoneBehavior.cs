using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneBehavior : MonoBehaviour, IFlockingBehavior
{
    public Vector3 center;
    public float radius;
    public float multiplier = 1;
    Vector3 dir;
    private void Start()
    {
        dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
    public Vector3 GetDir(List<FlockingManager> boids, FlockingManager entity)
    {
        var distance = Vector3.Distance(entity.GetEntity.transform.position, center);
        if (distance > radius) dir *= -1;
        return dir * multiplier;
    }
}
