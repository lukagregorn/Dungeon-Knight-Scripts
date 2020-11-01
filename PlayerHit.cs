﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{

    private GameObject player;

    void Start() {
        player = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        // check if enemy
        if (other.gameObject.CompareTag("Enemy") && other.isTrigger) {

            // get humanoid objects
            Humanoid playerHuman = player.GetComponent<Humanoid>();
            Humanoid enemyHuman = other.GetComponent<Humanoid>();

            // get values
            float thrust = playerHuman.GetThrust();
            float knockTime = playerHuman.GetKnockTime();
            int damage = playerHuman.GetDamage();

            // calculate knock vector
            Vector2 knockVector = enemyHuman.GetRigidbody().transform.position - transform.position;
            knockVector = knockVector.normalized * thrust;

            // inflict knockback and damage
            enemyHuman.TakeDamage(damage);
            enemyHuman.Knockback(knockVector, knockTime);
        }


        // check if breakable
        if (other.CompareTag("Breakable")) {
            other.GetComponent<Pot>().Smash();
        }
    }

    
}
