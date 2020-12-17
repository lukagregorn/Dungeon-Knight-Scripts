using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public IntValue playerCoins;
    public Text coinsText;

    private void Start() {
        UpdateCoins();
    }

    public void UpdateCoins() {
        coinsText.text = playerCoins.initialValue.ToString();
    }

}
