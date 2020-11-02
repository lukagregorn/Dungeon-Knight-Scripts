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

    private void Start() {
        InitHearts();
    }

    public void InitHearts() {

        int amount = maxHealth.initialValue / 2;

        for (int i = 0; i < amount; i++) {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }

        if (maxHealth.initialValue / 2 != 0) {
            hearts[amount + 1].gameObject.SetActive(true);
            hearts[amount + 1].sprite = halfFullHeart;
        }

    }
}
