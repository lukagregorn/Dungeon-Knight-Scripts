using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public IntValue maxHealth;
    public IntValue playerHealth;

    private void Start() {
        UpdateHearts();
    }

    public void UpdateHearts() {
        
        int totalAmount = maxHealth.value / 2;
        int fullAmount = playerHealth.value / 2;

        // set active heart containers
        for (int i = 0; i < totalAmount; i++) {
            hearts[i].gameObject.SetActive(true);
            if (i < fullAmount)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }

        if (playerHealth.value % 2 != 0) {
            hearts[fullAmount].sprite = halfFullHeart;
        }
        
    }

}
