using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class PowerUp : Entity
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.HitPowerUp();
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
