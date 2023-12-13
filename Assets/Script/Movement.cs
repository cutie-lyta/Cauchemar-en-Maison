using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float _speed = 1.0f; // vitesse du joueur
    [SerializeField] private Rigidbody _rb ; // on prend un rigidbody pour faire des collisions
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // on prend la direction
        _rb.velocity = transform.TransformDirection(dir * _speed); // on change la vélocité du rigid body pour faire déplacer le joueur en prenant en compte les collisions
    }
}
