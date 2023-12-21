using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBeforeExit : MonoBehaviour
{
    public GameObject Panel;
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private MovementBehaviour _movementBehaviour;

    // Start is called before the first frame update
    public void Quit()
    {
        FindObjectOfType<ObjectPositionner>().TimerFinished();
    }

    public void Continue()
    {
        Panel.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        _cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 1500;
        _cam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 1000;
        _movementBehaviour._speed = 8.0f;
    }
}
