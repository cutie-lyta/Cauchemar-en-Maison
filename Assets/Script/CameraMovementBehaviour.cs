using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovementBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _playerBody; // pour récupérer le transform du joueur

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
