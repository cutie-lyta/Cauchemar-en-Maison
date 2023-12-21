using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintDisplaced : MonoBehaviour

    
{
    [SerializeField] private ObjectPositionner _objectPositionner;

    // Update is called once per frame
    void Update()
    {
        int _nbDisplaced = _objectPositionner.Objets.Count;
        var textMesh = GetComponent<TextMeshProUGUI>();

        foreach (Objet obj in _objectPositionner.Objets)
        {
            if (!obj.pointer.CompareTag("Displaced"))
            {
                _nbDisplaced--;
                
            }
        }

        string _txtDisplaced = _nbDisplaced.ToString();
        textMesh.text = _txtDisplaced;
    }
}
