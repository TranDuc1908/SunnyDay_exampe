using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    public float JumpPower => 2;
    public float MoveSpeed => 5;
    public bool IsCrouching { get; private set; }
    public bool IsGrounded { get; private set; }

    public bool IsCrouchButtonHolding() => true;
    public bool IsJumpButtonPressed() => true;
    public bool CanJump() => IsGrounded;
    public bool CanMoveFlat() => IsGrounded;

    void Update()
    {
        CheckGrounded();
        CheckCrouch();

        if (CanJump() && IsJumpButtonPressed())
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (CanMoveFlat())
        {
            TryFlatMovement(Time.fixedDeltaTime);
        }
    }

    public void Jump()
    {
        IsGrounded = false;

        rb.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);
    }

    void CheckGrounded()
    {
        if (!IsGrounded)
        {
            // check if touching the ground
            if (rb.position.y <= 0)// just example
            {
                IsGrounded = true;
            }
        }
    }

    void CheckCrouch()
    {
        bool newCrouch = IsCrouchButtonHolding();
        if (newCrouch != IsCrouching)
        {
            IsCrouching = newCrouch;

            // switch animation, bla bla
        }
    }

    void TryFlatMovement(float dt)
    {
        float right = 0; // get input something like press A or D
        float front = 0; // get input something like press W or S
        if (right != 0 && front != 0)
        {
            Vector2 move = new Vector2(right, front) * MoveSpeed * dt;
            rb.MovePosition(rb.position + move);
        }
    }
}
