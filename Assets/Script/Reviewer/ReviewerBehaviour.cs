using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _displacedObjects;

    // Start is called before the first frame update
    void Start()
    {
        _displacedObjects = GameObject.FindGameObjectsWithTag("Displaced");
        Debug.Log("ListMade");
    }

}
