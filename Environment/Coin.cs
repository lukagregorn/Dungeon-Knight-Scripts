using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {        
        if (other.gameObject.CompareTag("Player") && other.isTrigger) {
            
            Player player = other.GetComponent<Player>();
            player.UpdateCoins(1);

            Destroy(gameObject);
        }
    }
}
