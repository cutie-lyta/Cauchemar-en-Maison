using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Note
{
    private GameObject _notedObject;
    private bool _isWronglyPlaced;

    public Note(GameObject notedObject)
    {
        _isWronglyPlaced = false;
        _notedObject = notedObject;
    }

    
}
