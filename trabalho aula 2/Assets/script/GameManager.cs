using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public NetworkVariable<int> pickupCount = new();


    string playerName;
    public static string PlayerName
    {
        get { return Instance.playerName; }
        set
        {
            Instance.playerName = value;
        }
    }




    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnServerStarted += OnServerStartedHandler;
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedHandler;
        }


    }

    [ServerRpc(RequireOwnership = false)]
    public void objetospegadosServerRpc()
    {

        pickupCount.Value += 1;
    }
    
    
    

    private void OnServerStartedHandler()
    {
        Debug.Log("Host conectado");
    }
    private void OnClientConnectedHandler(ulong clientId)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log($"Cliente {clientId} conectado");
        }
    }
    
    
}
