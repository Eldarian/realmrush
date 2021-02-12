using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    Queue<Tower> towers = new Queue<Tower>();
    [SerializeField] Tower towerPrefab;

    public void AddTower(Waypoint waypoint)
    {
        //var towers = FindObjectsOfType<Tower>();
        int numTowers = towers.Count;

        if(numTowers == towerLimit)
        {
            var tower = towers.Dequeue();
            MoveExistingTower(tower, waypoint);
        }
        else
        {
            InstantiateTower(waypoint);
        }
    }

    private void MoveExistingTower(Tower tower, Waypoint waypoint)
    {
        var oldWaypoint = tower.objectToPan;
        if (oldWaypoint != null)
        {
            oldWaypoint.GetComponent<Waypoint>().isPlaceable = true;
        }

        tower.objectToPan = waypoint.transform;
        tower.transform.position = waypoint.transform.position;
        towers.Enqueue(tower);
        waypoint.isPlaceable = false;

    }

    void InstantiateTower(Waypoint waypoint)
    {
        var tower = Instantiate(towerPrefab, waypoint.transform.position, towerPrefab.transform.rotation);
        MoveExistingTower(tower, waypoint);
    }
}
