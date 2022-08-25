using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBFSDFS : MonoBehaviour
{
    public LayerMask mask;
    public float distanceMax;
    public float radius;
    public Vector3 offset;
    private Node _init;
    private Node _finit;
    List<Node> _list;
    AStar<Node> _aStar = new AStar<Node>();
    
    public List<Node> PathFindingAStar(Node start, Node end)
    {
        _init = start;
        _finit = end;
        _list = _aStar.Run(_init, Satisfies, GetNeighbours, GetCost, Heuristic);
        return _list;
    }
    float Heuristic(Node curr)
    {
        float cost = 0;
        cost += Vector3.Distance(curr.transform.position, _finit.transform.position);
        return cost;
    }
    float GetCost(Node p, Node c)
    {
        return Vector3.Distance(p.transform.position, c.transform.position);
    }
    List<Node> GetNeighbours(Node curr)
    {
        return curr.neightbourds;
    }
    bool Satisfies(Node curr)
    {
        return curr == _finit;
    }
    
}
