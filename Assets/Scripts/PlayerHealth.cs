using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class PlayerHealth : NetworkBehaviour
{
    public NetworkVariableFloat health = new NetworkVariableFloat(100f);
    // Start is called before the first frame update
    
    public void TakeDamage(float damage)
    {
        health.Value -= damage;
    }
}
