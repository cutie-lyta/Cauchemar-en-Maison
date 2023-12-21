using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintDisplaced : MonoBehaviour

    
{
    [SerializeField] private ObjectPositionner _objectPositionner;
    private int _nbDisplaced;
    
    // Start is called before the first frame update
    void Start()
    {
        var textMesh = GetComponent<TextMesh>();

        _nbDisplaced = _objectPositionner.Objets.Count;
        string _txtDisplaced = _nbDisplaced.ToString();        
        textMesh.text = _txtDisplaced ;
    }

    // Update is called once per frame
    void Update()
    {
        var textMesh = GetComponent<TextMesh>();

        foreach (Objet obj in _objectPositionner.Objets)
        {
            if (obj.pointer.CompareTag("Displaced"))
            {
                _nbDisplaced--;
                string _txtDisplaced = _nbDisplaced.ToString();
                textMesh.text = _txtDisplaced ;
            }
        }
    }
}
