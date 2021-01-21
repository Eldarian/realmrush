using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderTest : MonoBehaviour
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

        while (reachable.Count > 0) //if there is nowhere to go stop search
        {
            ColorBlocks();
            var searchCenter = reachable.Dequeue();
            searchCenter.SetTopColor(Color.magenta);
            if (searchCenter == finish)
            {
                BuildPath();
                yield break;
            }
            //choose node
            FindNeighbours(searchCenter); //find neighbours of current element
            yield return new WaitForSeconds(0.5f);
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
    private void FindNeighbours(Waypoint searchCenter)
    {
        foreach (Vector2Int direction in directions)
        {
            try
            {
                Vector2Int neighbourCoordinates = searchCenter.GridPos + direction;
                Waypoint neighbour = grid[neighbourCoordinates];
                neighbour.SetTopColor(Color.blue);
                if (!reachable.Contains(neighbour) && !neighbour.isExplored)
                {
                    neighbour.previous = searchCenter; //set current element as previous for every neighbour
                    reachable.Enqueue(neighbour); //add current element to queue
                }
            }
            catch
            {

            }
        }

        return;
    }

    private void ColorBlocks()
    {
        foreach (Waypoint waypoint in grid.Values)
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
