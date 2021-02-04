using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secindsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Waypoint start, end;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab.gameObject, transform);
            EnemyMovement enemyMove = enemy.GetComponent<EnemyMovement>();
            enemyMove.SetStartEnd(start, end);

            yield return new WaitForSeconds(secindsBetweenSpawns);
        }

    }
}
