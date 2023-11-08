using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

public class personagem : NetworkBehaviour
{
    public float jumpForce = 5f;
    public bool canJump = true;
    private bool isGrounded = true;
    [SerializeField] TextMeshProUGUI displayName;
    public TMP_InputField nameInput; // Reference to the InputField for entering the name

    public NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>(string.Empty, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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
    // ...

    public void OnPlayerNameChanged(FixedString32Bytes previous, FixedString32Bytes current)
    {
        UpdateDisplay(current.ToString());
        Debug.Log($"OnPlayerNameChanged previous {previous}, current {current} value");
  }
    private void UpdateDisplay(string value)
    {
        displayName.text = value;
    }

    public void UpdatePlayerName()
    {
        string newPlayerName = nameInput.text;
        playerName.Value = newPlayerName;
        GameManager.PlayerName = newPlayerName;
    }

    void Update()
    {
        if (NetworkObject.IsOwner)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.position += move * Time.deltaTime * 5;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canJump && isGrounded)
                {
                    GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    canJump = false;
                    isGrounded = false;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            canJump = true;
        }
    }
}
