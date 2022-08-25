using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorBehavior : MonoBehaviour, IFlockingBehavior
{
    public float multiplier;
    public float radius;
    public LayerMask maskPredator;
    public Vector3 GetDir(List<FlockingManager> boids, FlockingManager entity)
    {
        Vector3 dir = Vector3.zero;
        Collider[] predators = Physics.OverlapSphere(entity.GetEntity.transform.position, radius, maskPredator);
        for (int i = 0; i < predators.Length; i++)
        {
            Vector3 posEntity = entity.GetEntity.transform.position;
            Vector3 posPredator = predators[i].ClosestPoint(posEntity);
            float distance = Vector3.Distance(posPredator, posEntity);
            Vector3 dirFlee = (posEntity - posPredator).normalized * (radius - distance);
            dir += dirFlee;
        }
        if (predators.Length != 0)
            dir /= predators.Length;
        return dir * multiplier;
    }
}
