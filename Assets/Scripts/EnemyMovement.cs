using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<Waypoint> path;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PrintAllWaypoints();
    }

    private void PrintAllWaypoints()
    {
        foreach (Waypoint waypoint in path)
        {
            print(waypoint.name);
        }
    }
}
