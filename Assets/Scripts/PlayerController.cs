using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    public Animator animator;
    public Gate currentGate;

    public GameObject xGate, xGateSelected;
    public GameManager gameManager; // Reference to your GameManager script

    // 0: totally outside
    // 1: inside, not collided with inner collider
    // 2: inside, collided with inner collider
    public int PlayerOnGate = 0;


    Vector2 velocity;

    private void Start()
    {
        currentGate = Gate.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGamePaused)
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
    }

    private void UpdateAnimation()
    {
        if (!gameManager.isGamePaused) 
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
        
    }

    private void UpdateMovement()
    {
        Vector2 currentPos = transform.position;
        transform.position = currentPos + velocity * moveSpeed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeadWall"))
        {
            gameManager.Lose();
        }
        else if (other.gameObject.CompareTag("OuterArea"))
        {
            PlayerOnGate = 1;
        }

        if (PlayerOnGate != 2)
        {
            if (other.gameObject.CompareTag("XGate"))
            {
                if (currentGate == Gate.None)
                {
                    currentGate = Gate.X;
                    xGateSelected.SetActive(true);
                    xGate.SetActive(false);
                    StopGameForSeconds(1f);
                }
                else if (currentGate == Gate.X)
                {
                    currentGate = Gate.None;
                    xGateSelected.SetActive(false);
                    xGate.SetActive(true);
                    StopGameForSeconds(1f);
                }
                PlayerOnGate = 2;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("OuterArea"))
        {
            if (PlayerOnGate == 1)
            {
                if (currentGate == Gate.None)
                {
                    currentGate = Gate.X;
                    xGateSelected.SetActive(true);
                    xGate.SetActive(false);
                    StopGameForSeconds(1f);
                }
                else if (currentGate == Gate.X)
                {
                    currentGate = Gate.None;
                    xGateSelected.SetActive(false);
                    xGate.SetActive(true);
                    StopGameForSeconds(1f);
                }
            }
            PlayerOnGate = 0;
        }
    }

    public void StopGameForSeconds(float seconds)
    {
        StartCoroutine(StopGameCoroutine(seconds));
    }

    private IEnumerator StopGameCoroutine(float seconds)
    {
        gameManager.StopWalking();


        yield return new WaitForSeconds(seconds);

        gameManager.isGamePaused = false;
    }
}

public enum Gate
{
    None,
    X, 
    H
}
