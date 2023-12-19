using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPlacementGenerator : MonoBehaviour
{
    [Tooltip("Lower Minimum + Lower Maximum = Higher difficulty")]
    [SerializeField] private Vector2 _range;
    [SerializeField] private int _numberOfDisplaced;
    [SerializeField] private List<GameObject> _stuffInScene;

    //private int _numberOfPointCheck = 5;
    void Awake()
    {
        _numberOfDisplaced = Mathf.Clamp(_numberOfDisplaced, 0, _stuffInScene.Count);
        // Get a List of all gameobject that will be displaced
        for (int i = 0; i < _numberOfDisplaced; i++)
        {
            var index = Random.Range(0, _stuffInScene.Count);
            var gameObject = _stuffInScene[index];
            _stuffInScene.RemoveAt(index);

            gameObject.tag = "Displaced";       
            
            var position = gameObject.transform.position;
            var rotation = gameObject.transform.rotation;
            var scale = gameObject.transform.localScale;

            gameObject.GetComponent<ObjectBehaviour>().OnCollisionEnterEvent += ObjectCollision;    //Used to reposition the object if out of bounds
            gameObject.GetComponent<ObjectBehaviour>().OnCollisionStayEvent += ObjectCollision;

            var socket = new GameObject();          //The object's initial position
            socket.transform.position = position;
            socket.transform.rotation = rotation;
            socket.transform.localScale = scale;

            var objet = new Objet { pointer = gameObject, transform = socket.transform };
            gameObject.GetComponent<ObjectBehaviour>().Socket = socket;
            

            gameObject.transform.position = MakeRandomVector(position);     //Randomly moves the object

            socket.transform.SetParent(GameObject.FindWithTag("Respawn").transform);
            
            FindObjectOfType<ObjectPositionner>().Objets.Add(objet);
        }

        foreach (var stuff in _stuffInScene)
        {
            if(stuff.GetComponent<Rigidbody>()) stuff.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    //Makes a random vector in the given range
    private Vector3 MakeRandomVector(Vector3 initPos)
    {
        return new Vector3(
                initPos.x + Random.Range(_range.x * RandomPosOrNeg(), _range.y * RandomPosOrNeg()),
                initPos.y,
                initPos.z + Random.Range(_range.x * RandomPosOrNeg(), _range.y * RandomPosOrNeg())
            );
    }

    //Randomly returns -1 or 1 (it uses Ternary function)
    private int RandomPosOrNeg()
    {
        return (Random.Range(-1, 1) >= 0) ? 1 : -1;
    }

    public void ObjectCollision(GameObject gObject, Collision collision)
    {
        if (collision.gameObject.CompareTag("Outside"))             //Reroll la position si l'objet est dehors
        {
            Debug.Log(gObject.name + " has been out");
            gObject.transform.position = gObject.GetComponent<ObjectBehaviour>().Socket.transform.position;

            gObject.transform.position = MakeRandomVector(gObject.transform.position);
        }

        else if (_stuffInScene.Contains(collision.gameObject))       //Place l'objet au dessus des autres objets de la scène
        {

            gObject.transform.position = new Vector3(   gObject.transform.position.x,
                                                        collision.collider.bounds.max.y,
                                                        gObject.transform.position.z
                                                        );

            gObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }

        else if (collision.gameObject.CompareTag("Sol"))            //Ne fais rien en cas de contact avec le sol
        {
            gObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            return;
        }

    }
}
