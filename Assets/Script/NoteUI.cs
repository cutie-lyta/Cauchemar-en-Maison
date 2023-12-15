using System;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteUI : MonoBehaviour
{
    [SerializeField] private CameraMovementBehaviour _camMovementBehaviour;
    [SerializeField] private ClockBehaviour _clockBehaviour;
    public event Action<GameObject> OnReport;
    private GameObject _hitted;

    private void Start()
    {
        HideNoteUI();
    }

    public void ShowNoteUI(GameObject gameObject)
    {
        _hitted = gameObject;
        this.gameObject.SetActive(true);
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        _camMovementBehaviour.CameraActive = false;
    }

    public void HideNoteUI()
    {
        this.gameObject.SetActive(false);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        _camMovementBehaviour.CameraActive = true;
    }

    public void Report()
    {
        HideNoteUI();

        OnReport?.Invoke(_hitted);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
