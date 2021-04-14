using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Transports.UNET;
using UnityEngine.UI;
using System;

public class MenuScript : MonoBehaviour
{
    public GameObject menuPanel;
    public InputField inputField;

    private void Start()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        bool approve = false;
        //if the connection data is correct then we approve 
        string password = System.Text.Encoding.ASCII.GetString(connectionData);
        if (password == "mygame")
        {
            approve = true;
        }
        Debug.Log($"Approval: {approve}");
        callback(true, null, approve, new Vector3(0, 10, 0), Quaternion.identity);
    }

    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        menuPanel.SetActive(false);
    }

    public void Join()
    {
        //clicked join
        if (inputField.text.Length <= 0)
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = "127.0.0.1";
        }
        else
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = inputField.text;
        }
        Debug.Log($"Connection Address: {NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress}");
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("mygame");
        NetworkManager.Singleton.StartClient();
        menuPanel.SetActive(false);
    }
}
