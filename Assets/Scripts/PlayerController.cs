using EasyJoystick;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    public Animator animator;
    public Gate currentGate;

    public GameObject[] xGate, xGateSelected, hGate, hGateSelected, yGate, yGateSelected;

    private GameManager gameManager; // Reference to your GameManager script

    public Joystick joystick;

    // 0: totally outside
    // 1: inside, not collided with inner collider
    // 2: inside, collided with inner collider
    public int PlayerOnGate = 0;

    public bool isLeft;

    Vector2 velocity;

    private void Start()
    {
        currentGate = Gate.None;
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.instance;
        }

        if (!gameManager.isGamePaused)
        {
            velocity = Vector2.zero;
            rb.angularVelocity = 0f;

            float horizontal, vertical;

            if (gameManager.isMobile)
            {
                horizontal = joystick.Horizontal();
                vertical = joystick.Vertical();
            }
            else
            {
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");
            }

            if (currentGate == Gate.None)
            {
                velocity.x = horizontal;
                velocity.y = vertical;
            }
            else if (currentGate == Gate.X || currentGate == Gate.Y)
            {
                velocity.x = -horizontal;
                velocity.y = -vertical;
            }
            else if (currentGate == Gate.H)
            {
                velocity.x = vertical;
                velocity.y = -horizontal;
            }
            else if (currentGate == Gate.XH)
            {
                velocity.x = -vertical;
                velocity.y = horizontal;
            }
            else if (currentGate == Gate.YH)
            {
                velocity.x = -vertical;
                velocity.y = horizontal;
            } 
            else if (currentGate == Gate.XY)
            {
                velocity.x = vertical;
                velocity.y = -horizontal;
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
        else if (other.gameObject.CompareTag("Finish"))
        {
            gameManager.PlayerWin(isLeft);
        }
        else if ((other.gameObject.CompareTag("OuterAreaX") || other.gameObject.CompareTag("OuterAreaH")) && PlayerOnGate != 2)
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
                    ChangeGate(Gate.X, other.gameObject, true);
                }
                else if (currentGate == Gate.X)
                {
                    currentGate = Gate.None;
                    ChangeGate(Gate.X, other.gameObject, false);
                }
                else if (currentGate == Gate.XH)
                {
                    currentGate = Gate.H;
                    ChangeGate(Gate.X, other.gameObject, false);
                }
                else if (currentGate == Gate.Y)
                {
                    currentGate = Gate.None;
                    ChangeGate(Gate.YH, other.gameObject, false);
                }
                PlayerOnGate = 2;
            }
            else if (other.gameObject.CompareTag("HGate"))
            {
                if (currentGate == Gate.None)
                {
                    currentGate = Gate.H;
                    ChangeGate(Gate.H, other.gameObject, true);
                }
                else if (currentGate == Gate.H)
                {
                    currentGate = Gate.None;
                    ChangeGate(Gate.H, other.gameObject, false);
                }
                else if (currentGate == Gate.X)
                {
                    currentGate = Gate.XH;
                    ChangeGate(Gate.H, other.gameObject, true);
                }
                else if (currentGate == Gate.XH)
                {
                    currentGate = Gate.X;
                    ChangeGate(Gate.H, other.gameObject, false);
                }
                else if (currentGate == Gate.YH)
                {
                    currentGate = Gate.Y;
                    ChangeGate(Gate.H, other.gameObject, false);
                }
                PlayerOnGate = 2;
            }
            else if (other.gameObject.CompareTag("YGate"))
            {
                if (currentGate == Gate.H)
                {
                    currentGate = Gate.YH;
                    ChangeGate(Gate.YH, other.gameObject, true);
                }
                else if (currentGate == Gate.XH)
                {
                    currentGate = Gate.H;
                    ChangeGate(Gate.YH, other.gameObject, false);
                    ChangeGate(Gate.X, other.gameObject, false);
                }
                else if (currentGate == Gate.YH)
                {
                    currentGate = Gate.H;
                    ChangeGate(Gate.YH, other.gameObject, false);
                }
                else if (currentGate == Gate.Y)
                {
                    currentGate = Gate.None;
                    ChangeGate(Gate.YH, other.gameObject, false);
                }
                PlayerOnGate = 2;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("OuterAreaX"))
        {
            if (PlayerOnGate == 1)
            {
                PlayerOnGate = 2;
                if (currentGate == Gate.None)
                {
                    currentGate = Gate.X;
                    ChangeGate(Gate.X, other.gameObject, true);
                }
                else if (currentGate == Gate.X)
                {
                    currentGate = Gate.None;
                    ChangeGate(Gate.X, other.gameObject, false);
                }
                else if (currentGate == Gate.H)
                {
                    currentGate = Gate.XH;
                    ChangeGate(Gate.X, other.gameObject, true);
                }
                else if (currentGate == Gate.XH)
                {
                    currentGate = Gate.H;
                    ChangeGate(Gate.X, other.gameObject, false);
                } 
                else if (currentGate == Gate.Y)
                {
                    currentGate = Gate.None;
                    ChangeGate(Gate.YH, other.gameObject, false);
                }

            } else
                PlayerOnGate = 0;
        }
        else if (other.gameObject.CompareTag("OuterAreaH"))
        {
            if (PlayerOnGate == 1)
            {
                PlayerOnGate = 2;
                if (currentGate == Gate.None)
                {
                    currentGate = Gate.H;
                    ChangeGate(Gate.H, other.gameObject, true);
                }
                else if (currentGate == Gate.H)
                {
                    currentGate = Gate.None;
                    ChangeGate(Gate.H, other.gameObject, false);
                }
                else if (currentGate == Gate.X)
                {
                    currentGate = Gate.XH;
                    ChangeGate(Gate.H, other.gameObject, true);
                }
                else if (currentGate == Gate.XH)
                {
                    currentGate = Gate.X;
                    ChangeGate(Gate.H, other.gameObject, false);
                }
                else if (currentGate == Gate.YH)
                {
                    currentGate = Gate.Y;
                    ChangeGate(Gate.H, other.gameObject, false);
                }


            }
            else
                PlayerOnGate = 0;
        }
        else if (other.gameObject.CompareTag("OuterAreaY"))
        {
            if (PlayerOnGate == 1)
            {
                PlayerOnGate = 2;
                if (currentGate == Gate.H)
                {
                    currentGate = Gate.YH;
                    ChangeGate(Gate.YH, other.gameObject, true);
                }
                else if (currentGate == Gate.XH)
                {
                    currentGate = Gate.H;
                    ChangeGate(Gate.YH, other.gameObject, false);
                    ChangeGate(Gate.X, other.gameObject, false);
                }
                else if (currentGate == Gate.YH)
                {
                    currentGate = Gate.H;
                    ChangeGate(Gate.YH, other.gameObject, false);
                }
                else if (currentGate == Gate.Y)
                {
                    currentGate = Gate.None;
                    ChangeGate(Gate.YH, other.gameObject, false);
                }

            }
            else
                PlayerOnGate = 0;
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            if (isLeft)
                gameManager.leftWin = false;
            else
                gameManager.rightWin = false;
        }
    }

    private void ChangeGate(Gate gate, GameObject other, bool enabled)
    {
        if (gate == Gate.X)
        {
            for (int i = 0; i < xGateSelected.Length; i++)
                xGateSelected[i].SetActive(enabled);

            for (int i = 0; i < xGate.Length; i++)
                xGate[i].SetActive(!enabled);
        }
        else if (gate == Gate.H)
        {
            for (int i = 0; i < hGateSelected.Length; i++)
                hGateSelected[i].SetActive(enabled);

            for (int i = 0; i < hGate.Length; i++)
                hGate[i].SetActive(!enabled);
        }
        else if (gate == Gate.YH)
        {
            for (int i = 0; i < yGateSelected.Length; i++)
                yGateSelected[i].SetActive(enabled);

            for (int i = 0; i < yGate.Length; i++)
                yGate[i].SetActive(!enabled);
        }
        


        // Get the child GameObject named "shader"
        GameObject shaderGameObject = other.transform.parent.gameObject.transform.Find("Shader").gameObject;

        // Check if the "shader" GameObject was found
        if (shaderGameObject != null)
        {
            shaderGameObject.SetActive(true);
        }

        gameManager.UIManager.catMeow.Play();

        gameObject.transform.position = new Vector2(other.transform.position.x, other.transform.position.y + 0.4f);

        StopGameForSeconds(1f, shaderGameObject);
    }

    public void StopGameForSeconds(float seconds, GameObject shader)
    {
        StartCoroutine(StopGameCoroutine(seconds, shader));
    }

    private IEnumerator StopGameCoroutine(float seconds, GameObject shader)
    {
        gameManager.PauseGame();


        yield return new WaitForSeconds(seconds);

        gameManager.ResumeGame();
        shader.SetActive(false);
    }
}

public enum Gate
{
    None,
    X, 
    H,
    XH,
    YH,
    XY,
    Y
}
