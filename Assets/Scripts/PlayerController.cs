using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;

    public Animator animator;
    public Gate currentGate;

    Vector2 velocity;

    private void Start()
    {
        currentGate = Gate.None;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        if (currentGate == Gate.None)
        {
            velocity.x = Input.GetAxisRaw("Horizontal");
            velocity.y = Input.GetAxisRaw("Vertical");
        }
        else if (currentGate == Gate.X)
        {
            velocity.x = -Input.GetAxisRaw("Horizontal");
            velocity.y = -Input.GetAxisRaw("Vertical");
        }
        
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (velocity != Vector2.zero)
        {
            UpdateMovement();

            animator.SetBool("Walking", true);
            animator.SetFloat("Horizontal", velocity.x);
            animator.SetFloat("Vertical", velocity.y);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void UpdateMovement()
    {
        Vector2 currentPos = transform.position;
        transform.position = currentPos + velocity * moveSpeed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("XGate"))
        {
            if (currentGate == Gate.None)
                currentGate = Gate.X;
            else if (currentGate == Gate.X)
                currentGate = Gate.None;
        }
    }
}

public enum Gate
{
    None,
    X, 
    H
}
