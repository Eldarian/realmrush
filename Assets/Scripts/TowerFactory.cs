using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;

    public void AddTower(Waypoint waypoint)
    {
        var towers = FindObjectsOfType<Tower>();
        int numTowers = towers.Length;

        if(numTowers == towerLimit)
        {
            MoveExistingTower(waypoint);
        }
        else
        {
            InstantiateTower(waypoint);
        }
    }

    private void MoveExistingTower(Waypoint waypoint)
    {
        Debug.Log("Max Towers Reached");
    }

    void InstantiateTower(Waypoint waypoint)
    {
        var tower = Instantiate(towerPrefab, waypoint.transform.position, towerPrefab.transform.rotation);
        tower.GetComponent<Tower>().objectToPan = waypoint.transform;
        waypoint.isPlaceable = false;
    }
}
