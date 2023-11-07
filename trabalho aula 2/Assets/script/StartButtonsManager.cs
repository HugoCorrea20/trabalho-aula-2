using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class StartButtonsManager : MonoBehaviour
{
    public GameObject hostbutton;
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        hostbutton.SetActive(false);
    }
    public void StartClient()
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.StartClient();
        }
    }

}
