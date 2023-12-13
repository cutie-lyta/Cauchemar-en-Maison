using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour
{
    [SerializeField] private List<Note> _notes = new();

    //temporary variable
    public GameObject selectedObject;
    public bool temporaryUI;

    public void MakeNewNote()
    {
        _notes.Add(new Note(selectedObject));
    }

    private void Start()
    {
        MakeNewNote();
        Debug.Log("NoteCreated");
    }
}
