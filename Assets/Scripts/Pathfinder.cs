using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    [SerializeField] Waypoint start;
    [SerializeField] Waypoint finish;

    Queue<Waypoint> reachable = new Queue<Waypoint>();
    HashSet<Waypoint> cameFrom;

    List<Waypoint> path;

    Vector2Int[] directions =
    {
        new Vector2Int(-1, 0),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(0, 1)
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        ColorBlocks();
        FindPath();

    }

    private void FindPath()
    {
        path = new List<Waypoint>();
        reachable.Enqueue(start); //adding first element to queue
        cameFrom = new HashSet<Waypoint>(); //initializing cameFrom set 

        while (reachable.Count>0) //if there is nowhere to go stop search
        {

            ColorBlocks(); //repaint to default colours
            var current = reachable.Dequeue();
            if (current == finish)
            {
                BuildPath();
                return;
            }
            //choose node
            List<Waypoint> neighbours = FindNeighbours(current); //find neighbours of current element

            foreach(Waypoint waypoint in neighbours) //for every neighbour
            {
                waypoint.previous = current; //set current element as previous for every neighbour
                reachable.Enqueue(waypoint); //add current element to queue

            }
            //from queue to set
            cameFrom.Add(current); //adding current element to history
        }
        

    }

    private void BuildPath()
    {
        print("finish");
        Waypoint current = finish;
        while (current.previous != null)
        {
            path.Add(current);
            current.SetTopColor(Color.white);
            current = current.previous;
        }
    }

    //Iteration between possible neighbours
    private List<Waypoint> FindNeighbours(Waypoint waypoint)
    {
        List<Waypoint> neighbours = new List<Waypoint>();
        foreach (Vector2Int direction in directions)
        {
            Vector2Int gridPos = waypoint.GridPos + direction;
            if (grid.ContainsKey(gridPos) && !cameFrom.Contains(grid[gridPos]))
            {
                neighbours.Add(grid[gridPos]);
                grid[gridPos].SetTopColor(Color.blue);
            }
        }

        return neighbours;
    }

    private void ColorBlocks()
    {
        foreach(Waypoint waypoint in grid.Values)
        {
            waypoint.SetTopColor(Color.cyan);
        }
        start.SetTopColor(Color.green);
        finish.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GridPos);
            if (!isOverlapping)
            {
                grid.Add(waypoint.GridPos, waypoint);
            }
            else
            {
                Debug.LogWarning("Blocks are overlapping on " + waypoint.GridPos);
            }
        }
        print(grid.Count);
    }

}
