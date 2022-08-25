using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlockingBehavior 
{
    Vector3 GetDir(List<FlockingManager> boids, FlockingManager entity);
}
