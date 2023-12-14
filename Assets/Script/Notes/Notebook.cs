using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour
{
    [field: SerializeField] public List<Note> Notes {get; private set;}

    [SerializeField] private RaycastBehaviour _raycastBehaviour;
    [SerializeField] private NoteUI _noteUI;
    public void MakeNewNote(GameObject objet)
    {
        Notes.Add(new Note(objet));
    }

    private void OnHitReceived(GameObject gameObject)
    {
        _noteUI.ShowNoteUI(gameObject);
    }

    private void OnReportReceived(GameObject hittedObj)
    {
        MakeNewNote(hittedObj);
    }

    private void Start()
    {
        Notes = new();
        _raycastBehaviour.OnHit += OnHitReceived;
        _noteUI.OnReport += OnReportReceived;
    }
}
