using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f; //  sensibilit� de la souris
    [SerializeField] private Transform playerBody; // pour r�cup�rer le transform du joueur
    float xRotation = 0f; // rotation de base (au d�but on est tout droit)
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // pour pas que le curseur bouge
    }

    // Update is called once per frame
    void Update()
    {
        // on prend les axes pr�-enregistr�s
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // on prend la rotation apr�s le mouvement (n�gatif si vers le haut et positif si vers le bas)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // on limite la rotation (on ne peut pas faire un tour complet en pitch)

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // on modifie la rotation dans le transform avec les quaternions
        playerBody.Rotate(Vector3.up * mouseX); // on ouge �galement le joueur (qui sera reli� � la cam�ra)
    }
}
