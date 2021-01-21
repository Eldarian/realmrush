using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    Waypoint start;
    Waypoint finish;

    Queue<Waypoint> reachable = new Queue<Waypoint>();

    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions =
    {
        new Vector2Int(-1, 0),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(0, 1)
    };

    private void BreadthFirstSearch()
    {
        reachable.Enqueue(start);
        print("finding path from" + start + " to " + finish);
        while (reachable.Count>0) //if there is nowhere to go stop search
        {
            var searchCenter = reachable.Dequeue();
            print(reachable.Count);

            if (searchCenter == finish)
            {
                BuildPath();
                return;            }
            //choose node
            FindNeighbours(searchCenter); //find neighbours of current element
            searchCenter.isExplored = true;
        }


    }

    public List<Waypoint> FindPath(Waypoint start, Waypoint finish)
    {
        this.start = start;
        this.finish = finish;
        LoadBlocks();
        //ColorBlocks();
        BreadthFirstSearch();
        return path;
    }
    private void BuildPath()
    {
        print("finish");
        Waypoint current = finish;
        while (current.previous != null)
        {
            path.Add(current);
            //current.SetTopColor(Color.white);
            current = current.previous;
        }
        path.Add(start);
        path.Reverse();
    }

    //Iteration between possible neighbours
    private void FindNeighbours(Waypoint searchCenter)
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GridPos + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                EnqueueNeighbour(searchCenter, neighbourCoordinates);
            }
        }

        return;
    }

    private void EnqueueNeighbour(Waypoint searchCenter, Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (!reachable.Contains(neighbour) && !neighbour.isExplored)
        {
            neighbour.previous = searchCenter; //set current element as previous for every neighbour
            reachable.Enqueue(neighbour); //add current element to queue
        }
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
