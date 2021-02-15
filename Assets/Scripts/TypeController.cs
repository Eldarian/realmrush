using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeController : MonoBehaviour
{
    private enum WaypointType { Friendly, Enemy, Neutral }
    [SerializeField] WaypointType type = WaypointType.Neutral;
    MeshFilter blockPrefab;
    [SerializeField] BlockProperties properties;

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
