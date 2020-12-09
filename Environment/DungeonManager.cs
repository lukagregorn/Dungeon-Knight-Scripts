using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : MonoBehaviour
{
    // spawners
    public int currentWave;
    public int enemiesToKill;
    public bool waveFinished;
    public List<Transform> spawnPoints;
    public IntValue currentTotalEnemies;
    public IntValue maxTotalEnemies;

    // enemies
    public List<GameObject> enemyPrefabs;
    public Transform enemyParent;
    public float spawnRate = 2.5f;

    // ui
    public Text waveText;
    public GameObject waveTextHolder;
    public Text enemiesLeftText;
    public GameObject enemiesLefTextHolder;


    // Start is called before the first frame update
    void Start()
    {
        waveTextHolder.SetActive(true);

        currentWave = 1;

        // start wave
        StartCoroutine(PrepWaveCoroutine());
    }

    private int GetEnemiesToKill() {
        return currentWave * 10 + (int)Mathf.Pow((currentWave-1), 2) * 5;
    }


    private int GetMaxTotalEnemies() {
        return 2 * currentWave + 5;
    }


    private void WaveComplete() {
        waveFinished = true;

        // show wave cleared
        enemiesLefTextHolder.SetActive(false);
        waveText.text = "WAVE " + currentWave.ToString() + " CLEARED";

        currentWave += 1;

        // prep and start next wave
        StartCoroutine(PrepWaveCoroutine());
    }



    // SPAWNERS
    private void SpawnEnemy() {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0,enemyPrefabs.Count)];
        Transform spawnpoint = spawnPoints[Random.Range(0,spawnPoints.Count)];
        if (enemyPrefab && spawnpoint) {
            currentTotalEnemies.value += 1;
            GameObject enemy = Instantiate(enemyPrefab, spawnpoint.position, Quaternion.identity, enemyParent);
            enemy.GetComponent<Enemy>().DiedEvent.AddListener(EnemyDiedHandler);
        }
    }


    private IEnumerator PrepWaveCoroutine() {
                
        yield return new WaitForSeconds(2.5f);

        // start next wave
        waveFinished = false;
        enemiesToKill = GetEnemiesToKill();
        maxTotalEnemies.value = GetMaxTotalEnemies();
        currentTotalEnemies.value = 0;
        
        // show new enemies to kill
        waveText.text = "WAVE " + currentWave.ToString();
        enemiesLeftText.text = "ENEMIES: " + enemiesToKill.ToString();
        enemiesLefTextHolder.SetActive(true);
        

        StartCoroutine(StartWaveCoroutine());
    }

    private IEnumerator StartWaveCoroutine() {
        while (gameObject.activeInHierarchy) {
            if (waveFinished) {
                break;
            }

            yield return new WaitForSeconds(spawnRate);
            if (currentTotalEnemies.value < maxTotalEnemies.value && 
                    enemiesToKill - currentTotalEnemies.value > 0 && !waveFinished) {

                SpawnEnemy();
            }

        }
    }


    private void EnemyDiedHandler() {
        currentTotalEnemies.value -= 1;
        enemiesToKill -= 1;

        enemiesLeftText.text = "ENEMIES: " + enemiesToKill.ToString();
        
        if (enemiesToKill == 0) {
            WaveComplete();
        }
    }

}
