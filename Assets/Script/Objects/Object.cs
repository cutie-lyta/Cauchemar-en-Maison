using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectScriptableObject", order = 1)]

public class Object : ScriptableObject
{
    public GameObject obj;
    public bool isDisplaced;
}
