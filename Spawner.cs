using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public IntValue currentTotalEnemies;
    public IntValue maxTotalEnemies;
    public Transform parent;
    public float spawnRate;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void SpawnEnemy() {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0,enemyPrefabs.Count)];
        if (enemyPrefab) {
            currentTotalEnemies.value += 1;
            GameObject enemy = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity, parent);
            enemy.GetComponent<Enemy>().DiedEvent.AddListener(EnemyDiedHandler);

        }
    }


    private IEnumerator SpawnCoroutine() {
        while (this.gameObject.activeInHierarchy) {
            yield return new WaitForSeconds(spawnRate);
            Debug.Log(currentTotalEnemies.value);
            Debug.Log(maxTotalEnemies.value);
            if (currentTotalEnemies.value < maxTotalEnemies.value) {
                Debug.Log("Got through");
                SpawnEnemy();
            }

        }
    }


    private void EnemyDiedHandler() {
        Debug.Log("Enemy died");
        currentTotalEnemies.value -= 1;
    }


}
