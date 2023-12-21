using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MalusTimer : MonoBehaviour
{
    [SerializeField] private RaycastBehaviour _raycastBehaviour;

    public void Hitted(GameObject @object)
    {
        var textMesh = GetComponentInChildren<TextMeshProUGUI>();

        if (!@object.CompareTag("Displaced") && @object.GetComponent<ObjectBehaviour>())
        {
            ObjectPositionner.Milliseconds += 5*1000;
            textMesh.text = "-5 sec";
            GetComponent<Animator>().SetTrigger("Malused"); // pour trigger l'animation dans l'animator (paramètre trigger)

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        _raycastBehaviour.OnHit += Hitted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
