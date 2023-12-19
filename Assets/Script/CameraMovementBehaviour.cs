using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 100f; //  sensibilité de la souris
    [SerializeField] private float _smooth = 0.2f;
    [SerializeField] private Transform _playerBody; // pour récupérer le transform du joueur
    float xRotation = 0f; // rotation de base (au début on est tout droit)
    private Vector3 _velocity = Vector3.zero;

    private int interpolationFramesCount = 45;
    private float elapsedFrames = 0;
    // Start is called before the first frame update
    public bool CameraActive { private get; set; } = true;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // pour pas que le curseur bouge
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (CameraActive)
        {
            _playerBody.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // on bouge également le joueur (qui sera relié à la caméra)
            
            // float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

            //Vector3 interpolatedPosition = Vector3.Slerp(this.transform.position, _playerBody.position, interpolationRatio);

            //this.transform.position = interpolatedPosition;

            //elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);

            //transform.position = Vector3.SmoothDamp(transform.position, _playerBody.position, ref _velocity,_smooth);


        }

    }


}
