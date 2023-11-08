using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Arraste o objeto de UI Text para esta vari�vel no Unity Inspector.
    public GameManager gameManager; // Arraste o objeto GameManager para esta vari�vel no Unity Inspector.

    private void Update()
    {
        if (gameManager != null && scoreText != null)
        {
            scoreText.text = "Pontua��o: " + gameManager.pickupCount.Value.ToString();
        }
    }
}
