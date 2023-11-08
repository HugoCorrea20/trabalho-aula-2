using Unity.Netcode;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public float pickupRange = 2.0f; // Dist�ncia m�xima para pegar o objeto
    public static ObjectPickup Instance;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupObject();
        }
    }

    private void TryPickupObject()
    {
        // Encontre todos os jogadores automaticamente usando a tag
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                // Calcule a dist�ncia entre o jogador e o objeto
                float distanceToPlayer = Vector3.Distance(players[i].transform.position, transform.position);
                Debug.Log("perto");
                if (distanceToPlayer <= pickupRange)
                {
                    // O jogador est� dentro do alcance do objeto, ent�o pegue-o
                    PickupObject(players[i]);
                }
            }
        }
    }

    private void PickupObject(GameObject player)
    {
        // Execute a l�gica de pegar o objeto, por exemplo:
        // Desative o objeto, aumente a contagem, etc.

        gameObject.SetActive(false);

        GameManager.Instance.objetospegadosServerRpc();
        // Voc� pode chamar uma fun��o no jogador para atualizar a contagem


        Debug.Log("Objeto pego!");
    }

}
