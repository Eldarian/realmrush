using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Waypoint currentPosition;
    [SerializeField] Waypoint destination;


    Pathfinder pathfinder;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = GetComponent<Pathfinder>();
        var path = pathfinder.FindPath(currentPosition, destination);
        StartCoroutine(GoThroughPath(path));
    }


    IEnumerator GoThroughPath(List<Waypoint> path)
    {
        print(gameObject.name + " started");
        foreach (Waypoint waypoint in path)
        {
            Transform nextStep = waypoint.transform;
            if (transform.position != nextStep.position)
            {
                transform.LookAt(nextStep);
            }

            transform.position = nextStep.position;
            yield return new WaitForSeconds(0.5f);
        }
    }


    public void SetStartEnd(Waypoint start, Waypoint end)
    {
        currentPosition = start;
        destination = end;
    }


}
