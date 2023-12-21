using Cinemachine;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class MovementBehaviour : MonoBehaviour
{

    [SerializeField] public float _speed = 8.0f; // vitesse du joueur
    [SerializeField] private Rigidbody _rb ; // on prend un rigidbody pour faire des collisions
    [SerializeField] private Transform rotationVector;
    [SerializeField] private CinemachineVirtualCamera _cam ;
    
    private bool _isCurrentlyColliding = false;

    void OnCollisionEnter(Collision col) {
        _isCurrentlyColliding = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            print("Trigger Enter Finish");
            FindObjectOfType<CheckBeforeExit>().Panel.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
            _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
            _speed = 0f;

        }
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
    void Update()
    {
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // on prend la direction
        dir = Vector3.ClampMagnitude(dir, 1);
        _rb.velocity = rotationVector.TransformDirection(dir*_speed); // on change la v�locit� du rigid body pour faire d�placer le joueur en prenant en compte les collisions

        var source = this.GetComponent<AudioSource>();

        source.pitch = 0.5f + dir.magnitude/2;

        if (dir.magnitude <= 0.1f || _isCurrentlyColliding) source.Pause();
        else source.UnPause();
        
        
    }
}
