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

    List<Waypoint> path = new List<Waypoint>();

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
        StartCoroutine(FindPath());

    }

    private IEnumerator FindPath()
    {
        reachable.Enqueue(start); 

        while (reachable.Count>0) //if there is nowhere to go stop search
        {

            ColorBlocks(); //repaint to default colours
            var searchCenter = reachable.Dequeue();
            searchCenter.SetTopColor(Color.magenta);

            if (searchCenter == finish)
            {
                BuildPath();
                yield break;
            }
            //choose node
            List<Waypoint> neighbours = FindNeighbours(searchCenter); //find neighbours of current element

            foreach(Waypoint waypoint in neighbours) //for every neighbour
            {
                waypoint.previous = searchCenter; //set current element as previous for every neighbour
                waypoint.SetTopColor(Color.blue);
                yield return new WaitForSeconds(0.5f);


                reachable.Enqueue(waypoint); //add current element to queue

            }
            searchCenter.isExplored = true;
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
            try
            {
                Vector2Int neighbourCoordinates = waypoint.GridPos + direction;
                Waypoint neighbour = grid[neighbourCoordinates];
                if (grid.ContainsKey(neighbourCoordinates) && !neighbour.isExplored)
                {
                    neighbours.Add(grid[neighbourCoordinates]);
                }
            }
            catch
            {

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
