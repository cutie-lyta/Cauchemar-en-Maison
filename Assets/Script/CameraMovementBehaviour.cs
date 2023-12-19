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
            // on prend les axes pré-enregistrés
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * 0.01f /* Time.deltaTime */;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * 0.01f /* Time.deltaTime */;

            xRotation -= mouseY; // on prend la rotation après le mouvement (négatif si vers le haut et positif si vers le bas)
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // on limite la rotation (on ne peut pas faire un tour complet en pitch)

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // on modifie la rotation dans le transform avec les quaternions

            _playerBody.Rotate(Vector3.up * mouseX); // on bouge également le joueur (qui sera relié à la caméra)

            //float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

            //Vector3 interpolatedPosition = Vector3.Slerp(this.transform.position, _playerBody.position, interpolationRatio);

            //this.transform.position = interpolatedPosition;

            //elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);

            //transform.position = Vector3.SmoothDamp(transform.position, _playerBody.position, ref _velocity,_smooth);


        }
            
    }


}
