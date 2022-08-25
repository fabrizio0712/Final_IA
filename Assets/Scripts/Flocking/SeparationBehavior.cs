using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparationBehavior : MonoBehaviour, IFlockingBehavior
{
    public float multiplier = 1;
    public float radiusPersonal;
    public Vector3 GetDir(List<FlockingManager> boids, FlockingManager entity)
    {
        if (boids.Count == 0) return Vector3.zero;
        int count = 0;
        Vector3 dir = Vector3.zero;
        for (int i = 0; i < boids.Count; i++)
        {
            Vector3 posEntity = entity.GetEntity.transform.position;
            Vector3 posBoid = boids[i].GetEntity.transform.position;
            float distance = Vector3.Distance(posBoid, posEntity);
            if (distance >= radiusPersonal) continue;
            count++;
            Vector3 dirFlee = (posEntity - posBoid).normalized * (radiusPersonal - distance);
            dir += dirFlee;
        }
        if (count != 0)
            dir /= count;

        return dir * multiplier;
    }
}
