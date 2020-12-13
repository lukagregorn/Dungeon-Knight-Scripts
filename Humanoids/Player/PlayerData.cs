using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // player stats
    public int healthLevel;
    public int strenghtLevel;
    public int speedLevel;

    // currency
    public int coins;

    // top wave
    public int bestWave;

    public PlayerData(Player player) {
        healthLevel = player.healthLevel.initialValue;
        strenghtLevel = player.strenghtLevel.initialValue;
        speedLevel = player.speedLevel.initialValue;

        coins = player.coins.initialValue;
        bestWave = player.bestWave.initialValue;
    }
}
