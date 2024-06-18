using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGamePaused;

    public PlayerController first, second;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win()
    {
        Debug.Log("Ha Ha You Win! :>");
    }

    public void Lose()
    {
        Debug.Log("Sorry U Lost! :<");
    }

    public void StopWalking()
    {
        first.animator.SetBool("Walking", false);
        second.animator.SetBool("Walking", false);
        isGamePaused = true;
    }

}
