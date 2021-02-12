using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridSize = 10;

    public Waypoint previous;
    public bool isExplored = false;
    [SerializeField] Tower towerPrefab;

    public bool isPlaceable = true;


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
        if(Input.GetMouseButtonDown(0) && isPlaceable)
        {
            FindObjectOfType<TowerFactory>().AddTower(this);
        }
   }
}
