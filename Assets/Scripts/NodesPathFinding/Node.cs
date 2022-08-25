using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neightbourds;
    [SerializeField] private float _maxDistance;
    private void Start()
    {
        GetNeightbourd(Vector3.right);
        GetNeightbourd(Vector3.left);
        GetNeightbourd(Vector3.forward);
        GetNeightbourd(Vector3.back);
    }
    void GetNeightbourd(Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, _maxDistance))
        {
            if(hit.collider.GetComponent<Node>())
            neightbourds.Add(hit.collider.GetComponent<Node>());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NPC>())
        {
            other.gameObject.GetComponent<NPC>().CurrentNode = this;
        }
    }
}
