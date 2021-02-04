using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridSize = 10;

    public Waypoint previous;
    public bool isExplored = false;

    [SerializeField] GameObject towerPrefab;

    public int GridSize => gridSize;
    public Vector2Int GridPos
    {
        get
        {
            return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
            );
        }
    }

   private void OnMouseOver()
   {
        if(Input.GetMouseButtonDown(0))
        {
            var tower = Instantiate(towerPrefab, transform.position, towerPrefab.transform.rotation);
            tower.GetComponent<Tower>().objectToPan = transform;
        }
   }
}
