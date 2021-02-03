using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secindsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;
    Transform enemyParent;
    [SerializeField] Waypoint start, end;

    // Start is called before the first frame update
    void Start()
    {
        enemyParent = GameObject.Find("Enemies").transform;
        StartCoroutine("SpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            GameObject enemy = Instantiate(enemyPrefab.gameObject, enemyParent);
            EnemyMovement enemyMove = enemy.GetComponent<EnemyMovement>();
            enemyMove.SetStartEnd(start, end);

            yield return new WaitForSeconds(secindsBetweenSpawns);
        }

    }
}
