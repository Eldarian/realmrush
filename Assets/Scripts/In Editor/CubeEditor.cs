using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[ExecuteInEditMode]
public class CubeEditor : MonoBehaviour
{
    [SerializeField]
    int gridSize = 10;
    TMP_Text label;

    void Start()
    {
        label = transform.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float xRounded = Mathf.RoundToInt(transform.position.x/gridSize)*gridSize;
        float zRounded = Mathf.RoundToInt(transform.position.z/gridSize)*gridSize;
        string gridPos = (xRounded / gridSize).ToString() + "," + (zRounded / gridSize).ToString();
        label.SetText(gridPos, true);
        gameObject.name = gridPos;
        transform.position = new Vector3(xRounded, transform.position.y, zRounded);
    }
}
