using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
internal class PlayerMotor : MonoBehaviour
{

    [SerializeField]
    private float _torque;
    [SerializeField]
    private float _torqueModifier;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpCooldown = .5f;
    private float _lastJumpTime = 0;
    [SerializeField]
    private float distanceToGround = 0.125f;

    private Transform feet;

    private float Torque
    {
        get { return controller.Inputs.sprinting ? _torque * _torqueModifier : _torque; }
    }

    public bool Grounded
    {
        get
        {
            return Physics.Raycast(feet.position, Vector3.down * distanceToGround, distanceToGround, LayerMask.GetMask("Floor"));
        }
    }

    public bool JumpOffCooldown
    {
        get
        {
            return Time.time >= _lastJumpTime + jumpCooldown;
        }
    }

    [SerializeField]
    private float _airModifier;

    private Rigidbody rb;
    private PlayerController controller;

    [SerializeField]
    [Range(0, 1)]
    private float brakeFrictionFactor;
    [SerializeField]
    [Range(0, 1)]
    private float constantFrictionFactor;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
        feet = transform.Find("Feet");
    }

    private void FixedUpdate()
    {
        HandleInputs(controller.Inputs);
    }

    private void HandleInputs(PlayerController.PlayerInputs inputs)
    {
        HandleMovementInputs(inputs);
    }
    public int jumps = 0;
    private void HandleMovementInputs(PlayerController.PlayerInputs inputs)
    {


        Vector3 movementForce = inputs.movement.x * transform.right + inputs.movement.z * transform.forward;

        bool grounded = Grounded;
        bool noInput = Approx(movementForce.magnitude, 0, .25f);

        movementForce *= Torque * Time.deltaTime;
        rb.velocity = new Vector3(movementForce.x, rb.velocity.y, movementForce.z);

        if (noInput)
        {
            ApplyBrakeFriction();
        }
        ApplyConstantFriction();
        

        if (inputs.jumping && Grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (JumpOffCooldown && Grounded)
        {
            _lastJumpTime = Time.time;
            jumps++;
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    private bool Approx(Vector3 a, Vector3 b, float maxDelta)
    {
        return Approx(a.x, b.x, maxDelta) && Approx(a.y, b.y, maxDelta) && Approx(a.z, b.z, maxDelta);
    }

    private bool Approx(float a, float b, float maxDelta)
    {
        return Mathf.Abs(a - b) <= maxDelta;
    }

    private void ApplyBrakeFriction()
    {
        float friction = 1 - brakeFrictionFactor;
        rb.velocity = new Vector3(rb.velocity.x * friction, rb.velocity.y, rb.velocity.z * friction);
    }

    private void ApplyConstantFriction()
    {
        float friction = 1 - constantFrictionFactor;
        rb.velocity = new Vector3(rb.velocity.x * friction, rb.velocity.y, rb.velocity.z * friction);
    }
}