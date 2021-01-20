using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<Waypoint> path;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoThroughPath());
    }


    IEnumerator GoThroughPath()
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
