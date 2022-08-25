using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody _rb;

    public Animator Anim { get => _anim; set => _anim = value; }

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 tempVel = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        _anim.SetFloat("Speed", tempVel.magnitude);
    }
}

