using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridSize = 10;

    public Waypoint previous;
    public bool isExplored = false;
    public BlockProperties properties;

    private enum WaypointType { Friendly, Enemy, Neutral }

    [SerializeField] WaypointType type = WaypointType.Neutral;
    MeshFilter blockPrefab;

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
        UpdateMeshByType();
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

    public void UpdateMeshByType() //todo implement mesh update
    {
        blockPrefab = transform.Find("Block").GetComponent<MeshFilter>();
        switch (type)
        {
            case WaypointType.Neutral:
                blockPrefab.mesh = properties.neutral;
                break;
            case WaypointType.Friendly:
                blockPrefab.mesh = properties.friend;
                break;
            case WaypointType.Enemy:
                blockPrefab.mesh = properties.enemy;
                break;
        }
    }
}
