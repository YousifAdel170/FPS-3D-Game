using UnityEngine;

public class EndGameBox : MonoBehaviour
{
    private InputManager Instance;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has interacted with the specific box, trigger end of the game
            InputManager.Instance.EndGame(); // Call a method to end the game, e.g., GameManager.Instance.EndGame();
        }
    }
}