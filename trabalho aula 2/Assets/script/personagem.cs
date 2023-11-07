using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

using Unity.Collections;

public class personagem : NetworkBehaviour
{
    public float jumpForce = 5f; // Variable to set jump force
    public bool canJump = true; // Variable to control if player can jump
    private bool isGrounded = true; // Variable to track if player is grounded
    [SerializeField] TextMeshProUGUI displayName;
    public GameObject pickupObject; // O objeto que o jogador pode pegar
    public  int pickupCount = 0;    // Contagem dos objetos pegos



    public NetworkVariable<FixedString32Bytes> playerName =new NetworkVariable<FixedString32Bytes> (string.Empty,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private void Start()
    {
        if (IsOwner)
        {
            playerName.Value = GameManager.PlayerName;
        }
    }
    public override void OnNetworkSpawn()
    {
        if (IsClient)
        {
            playerName.OnValueChanged += OnPlayerNameChanged;
            UpdateDisplay(playerName.Value.ToString());
        }
    }
    public override void OnNetworkDespawn()
    {
        playerName.OnValueChanged -= OnPlayerNameChanged;
       
    }
    public void OnPlayerNameChanged(FixedString32Bytes previous, FixedString32Bytes current)
    {
        UpdateDisplay(current.ToString());
        Debug.Log($"OnPlayerNameChanged previous {previous}, current {current} value");
}
    private void UpdateDisplay(string value)
    {
        displayName.text = value;
    }
    void Update()
    {
        if (NetworkObject.IsOwner)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.position += move * Time.deltaTime * 5;

            // Handle jumping
            if (Input.GetKeyDown(KeyCode.Space)) // Check if space key is pressed
            {
                if (canJump && isGrounded) // Check if the player can jump and is grounded
                {
                    GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply upward force to jump
                    canJump = false; // Set canJump to false so the player can't jump again
                    isGrounded = false; // Set isGrounded to false to track if player is still grounded
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryPickupObject();
            }
        }
        
    }
    void TryPickupObject()
    {
        if (pickupObject != null) // Verifica se há um objeto para pegar
        {
            float pickupRange = 2.0f; // Defina a distância máxima para pegar o objeto

            // Calcule a distância entre o jogador e o objeto
            float distanceToPickup = Vector3.Distance(transform.position, pickupObject.transform.position);

            if (distanceToPickup <= pickupRange)
            {
                // O jogador está dentro do alcance do objeto, então pegue-o
                PickupObject();
            }
        }
    }
    void PickupObject()
    {
        // Execute a lógica de pegar o objeto, por exemplo:
        // Desative o objeto, aumente a contagem, etc.

        pickupObject.SetActive(false); // Desativa o objeto (exemplo)

        // Atualize a contagem
        pickupCount++;
        Debug.Log("Objeto pego! Contagem: " + pickupCount);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") // Check if player collides with the ground
        {
            isGrounded = true; // Set isGrounded to true since player is grounded
            canJump = true; // Set canJump to true so player can jump again
        }
    }
}
