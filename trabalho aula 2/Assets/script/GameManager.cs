using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    string playerName;
    public static string PlayerName
    {
        get { return Instance.playerName;}
        set
        {
            Instance.playerName = value;
        }
    }
   public  static GameManager Instance;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
