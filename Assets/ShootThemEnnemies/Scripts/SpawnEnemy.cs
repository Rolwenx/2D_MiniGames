using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private float _spawnRate = 4f;
    [SerializeField] private GameObject[] _enemyPrefab;
    private bool _canSpawn = true;

    void Start(){
    StartCoroutine(Spawner());
    }
    
    private IEnumerator Spawner(){
        WaitForSeconds wait = new WaitForSeconds(_spawnRate);

        while (_canSpawn){
            yield return wait;
            int rand = Random.Range(0, _enemyPrefab.Length);
            GameObject enemyToSpawn = _enemyPrefab[rand];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
    }

}
