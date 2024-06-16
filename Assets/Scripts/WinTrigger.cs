using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameManager gameManager; // Reference to your GameManager script


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.CompareTag("Player"))
        {
            // Call the "Win()" function in your GameManager script
            gameManager.Win();
        }
    }
}