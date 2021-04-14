using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class PlayerHealth : NetworkBehaviour
{
    public NetworkVariableFloat health = new NetworkVariableFloat(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.ServerOnly }, 100f);
    MeshRenderer[] renderers;
    CharacterController cc;
    // Start is called before the first frame update
    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        cc = GetComponent<CharacterController>();

    }

    //running on the server
    public void TakeDamage(float damage)
    {
        health.Value -= damage;
        //check health

        if (health.Value <= 0)
        {

            health.Value = 100f;
            //respawn
            Vector3 pos = new Vector3(Random.Range(-10, 10), 4, Random.Range(-10, 10));
            ClientRespawnClientRpc(pos);
        }
    }

    [ClientRpc]
    void ClientRespawnClientRpc(Vector3 position)
    {
        StartCoroutine(Respawn(position));
    }

    IEnumerator Respawn(Vector3 position)
    {
        foreach(var renderer in renderers)
        {
            renderer.enabled = false;
        }

        cc.enabled = false;
        transform.position = position;
        //delay
        yield return new WaitForSeconds(1f);
        cc.enabled = true;
        foreach (var renderer in renderers)
        {
            renderer.enabled = true;
        }

    }
}
