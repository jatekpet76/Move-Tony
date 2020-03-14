using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgController : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var vert = Input.GetAxis("Vertical");
        var hori = Input.GetAxis("Horizontal");

        _animator.SetFloat("velV", vert);
        _animator.SetFloat("velH", hori);

        vert *= vert < 0 ? _speed * 0.5f : _speed;
        hori *= vert < 0 ? _speed * 0.5f : _speed;

        transform.position += Vector3.forward * vert * Time.deltaTime;
        transform.position += Vector3.right * hori * Time.deltaTime;
    }
}
