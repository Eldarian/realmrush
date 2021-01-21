using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Properties", menuName = "Properties", order = 51)]
public class BlockProperties : ScriptableObject
{
    public Mesh neutral;
    public Mesh enemy;
    public Mesh friend;
}
