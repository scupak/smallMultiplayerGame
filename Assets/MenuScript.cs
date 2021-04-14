using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class MenuScript : MonoBehaviour
{
    public GameObject menuPanel;
    // Start is called before the first frame update
    public void Host() {

        NetworkManager.Singleton.StartHost();
        menuPanel.SetActive(false);
    }

    public void Join()
    {

        NetworkManager.Singleton.StartClient();
        menuPanel.SetActive(false);
    }
}
