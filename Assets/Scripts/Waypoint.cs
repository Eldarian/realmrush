using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridSize = 10;

    public Waypoint previous;
    public bool isExplored = false;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer renderer = transform.Find("Top").GetComponent<MeshRenderer>();
        renderer.material.color = color;
    }
}
