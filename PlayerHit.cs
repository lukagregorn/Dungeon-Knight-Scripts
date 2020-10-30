using System.Collections;
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
        if (other.gameObject.CompareTag("Enemy")) {
            float thrust = player.GetComponent<Player>().thrust;
            float knockTime = player.GetComponent<Player>().knockTime;

            Vector2 knockVector = other.GetComponent<Rigidbody2D>().transform.position - transform.position;
            knockVector = knockVector.normalized * thrust;

            other.GetComponent<Enemy>().Knockback(knockVector, knockTime);
        }


        // check if breakable
        if (other.CompareTag("Breakable")) {
            other.GetComponent<Pot>().Smash();
        }
    }

    
}
