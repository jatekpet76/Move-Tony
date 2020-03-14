using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TonyController : MonoBehaviour
{
    [SerializeField] float _speed = 4;
    [SerializeField] MoveMode _moveMode = MoveMode.TRANSFORM_POSITION;

    Rigidbody _rigidbody;
    NavMeshAgent _agent;

    float _vertical = 0;
    float _horizontal = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        Move("Update");
    }
    void FixedUpdate()
    {
        Move("FixedUpdate");
    }

    void GetInput()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");
    }

    void Move(string when)
    {

        if (_moveMode == MoveMode.TRANSFORM_POSITION && when == "Update")
        {
            TransformPosition(_horizontal, _vertical);
        } else if (_moveMode == MoveMode.RB_FORCE && when == "FixedUpdate")
        {
            RigidBodyAddForce(_horizontal, _vertical);
        }
        else if (_moveMode == MoveMode.RB_POSITION && when == "FixedUpdate")
        {
            RigidBodyMovePosition(_horizontal, _vertical);
        }
    }

    // RB / Cons / Freeze Rotation X, Z ?
    void TransformPosition(float x, float z) {
        var pos = transform.position;

        transform.position = new Vector3(
            pos.x + (x * _speed * Time.deltaTime),
            pos.y,
            pos.z + (z * _speed * Time.deltaTime)
        );
    }

    void RigidBodyAddForce(float horizontal, float vertical)
    {
        var force = transform.forward * vertical * 10 * _speed;

        _rigidbody.AddForce(force);
        _rigidbody.AddTorque(new Vector3(0, horizontal * 0.5f * _speed, 0));
    }

    void RigidBodyMovePosition(float horizontal, float vertical)
    {
        var newPos = transform.forward * vertical * 10 * _speed;

        _rigidbody.MovePosition(newPos);
        // _rigidbody.AddTorque(new Vector3(0, horizontal * 0.5f * _speed, 0));
    }

    enum MoveMode
    {
        TRANSFORM_POSITION = 1,
        RB_FORCE = 2,
        RB_POSITION = 3,
        NAVMESH_POINT = 4
    }
}
