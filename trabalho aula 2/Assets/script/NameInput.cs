using UnityEngine;
using TMPro;

public class NameInput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayName;

    public void SetPlayerName(string playerName)
    {
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            GameManager.PlayerName = playerName;
            displayName.text = playerName;
        }
    }
}
