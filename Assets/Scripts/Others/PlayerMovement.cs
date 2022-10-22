using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header ("Parameters")]
    [SerializeField] private float _speedHor = 5f;
    [SerializeField] private float _speedVer = 5f;
    private bool isActive = true;

    [Header("Links")]
    private Rigidbody _rigidbody;
    private Player _player;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            Move();
        }
    }

    private void Move()
    {
        float movHor = Input.GetAxis("Horizontal") * _speedHor;
        float movVer = Input.GetAxis("Vertical") * _speedVer;

        Vector3 direction = new Vector3(movVer, 0, movHor);

        _rigidbody.MovePosition(transform.position + transform.TransformDirection(direction * Time.fixedDeltaTime));
    }
}
