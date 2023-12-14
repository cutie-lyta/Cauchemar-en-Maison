using System;
using UnityEngine;

[Serializable]
public class Note 
{
    private GameObject _notedObject;
    private bool _isWronglyPlaced;

    public Note(GameObject notedObject)
    {
        _notedObject = notedObject;
    }
    
}
