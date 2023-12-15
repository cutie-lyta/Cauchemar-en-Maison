using System;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{

    [SerializeField] private float _speed = 1.0f; // vitesse du joueur
    [SerializeField] private Rigidbody _rb ; // on prend un rigidbody pour faire des collisions

    private bool _isCurrentlyColliding = false;

    void OnCollisionEnter(Collision col) {
        _isCurrentlyColliding = true;
    }
 
    void OnCollisionExit(Collision col) {
        _isCurrentlyColliding = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // on prend la direction
        dir = Vector3.ClampMagnitude(dir, 1);
        _rb.velocity = transform.TransformDirection(dir * _speed) ; // on change la vélocité du rigid body pour faire déplacer le joueur en prenant en compte les collisions
        
        var source = this.GetComponent<AudioSource>();

        source.pitch = 0.5f + dir.magnitude/2;

        if (dir.magnitude <= 0.1f || _isCurrentlyColliding) source.Pause();
        else source.UnPause();
        
        
    }
}
