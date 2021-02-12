using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] GameObject towersParent;

    Queue<Tower> towers = new Queue<Tower>();


    public void AddTower(Waypoint waypoint)
    {
        int numTowers = towers.Count;

        var tower = (numTowers < towerLimit) ? InstantiateTower(waypoint) : towers.Dequeue();
        MoveExistingTower(tower, waypoint);
    }

    private void MoveExistingTower(Tower tower, Waypoint waypoint)
    {
        var oldWaypoint = tower.objectToPan;
        if (oldWaypoint != null)
        {
            oldWaypoint.GetComponent<Waypoint>().isPlaceable = true;
            tower.transform.position = waypoint.transform.position;
        }

        waypoint.isPlaceable = false;
        tower.objectToPan = waypoint.transform;
        towers.Enqueue(tower);

    }

    Tower InstantiateTower(Waypoint waypoint)
    {
        return Instantiate(towerPrefab, waypoint.transform.position, towerPrefab.transform.rotation, towersParent.transform);
    }
}
