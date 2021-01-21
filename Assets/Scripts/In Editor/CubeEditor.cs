using TMPro;
using UnityEngine;


[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    [SerializeField]
    const int gridSize = 10;
    TMP_Text label;

  

    Waypoint waypoint;


    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Start()
    {
        label = transform.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        SnapToGrid();
        UpdateLabel();
        waypoint.UpdateMeshByType();
    }

    private void SnapToGrid()
    {
        transform.position = new Vector3(waypoint.GridPos.x, 0f, waypoint.GridPos.y) * gridSize;
    }

    private void UpdateLabel()
    {
        string gridPosString = (waypoint.GridPos.x).ToString() + "," + (waypoint.GridPos.y).ToString();
        label.SetText(gridPosString, true);
        gameObject.name = gridPosString;
    }

   
}
