﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class PlayerShooting : NetworkBehaviour
{
    public ParticleSystem bulletParticleSystem;
    private ParticleSystem.EmissionModule em;

    NetworkVariableBool shooting = new NetworkVariableBool(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.OwnerOnly }, false);
    //bool shooting = false;
    float fireRate = 10f;
    float shootTimer = 0f;
    public AudioSource roar;


    // Start is called before the first frame update
    void Start()
    {
        em = bulletParticleSystem.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            shooting.Value = Input.GetMouseButton(0);
            shootTimer += Time.deltaTime;
            if (shooting.Value && shootTimer >= 1f / fireRate)
            {
                shootTimer = 0;
                ShootServerRpc();
                
            }


            if (shooting.Value) {
                if (!roar.isPlaying)
                {
                    roar.Play();
                }
            }
            else {
                roar.Stop();
            }
        }
        
        em.rateOverTime = shooting.Value ? fireRate : 0f;
    }

    [ServerRpc]
    void ShootServerRpc()
    {
        Ray ray = new Ray(bulletParticleSystem.transform.position, bulletParticleSystem.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            //we hit something
            var player = hit.collider.GetComponent<PlayerHealth>();
            if (player != null)
            {
                //we hit a player
                player.TakeDamage(10f);
            }
        }
    }
}
