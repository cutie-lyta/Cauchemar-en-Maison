using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPlacementGenerator : MonoBehaviour
{
    [Tooltip("Lower Minimum + Lower Maximum = Higher difficulty")]
    [SerializeField] private Vector2 _range;
    [SerializeField] private int _numberOfDisplaced;
    [SerializeField] private List<GameObject> _stuffInScene;

    private int _numberOfPointCheck = 5;
    void Awake()
    {
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

            gameObject.GetComponent<ObjectBehaviour>().OnCollisionEnterEvent += ObjectCollision;
            
            var socket = new GameObject();
            socket.transform.position = position;
            socket.transform.rotation = rotation;
            socket.transform.localScale = scale;

            var objet = new Objet { pointer = gameObject, transform = socket.transform };
            gameObject.GetComponent<ObjectBehaviour>().Socket = socket;
            
            Vector3 newPos;
            int tryNum = 0 ;
            
            newPos = new Vector3(
                position.x + (Random.Range(_range.x, _range.y) * Random.Range(0, 2)),
                position.y,
                position.z + Random.Range(_range.x, _range.y)
            );

            gameObject.transform.position = newPos;
            
            socket.transform.SetParent(GameObject.FindWithTag("Respawn").transform);
            
            FindObjectOfType<ObjectPositionner>().Objets.Add(objet);
        }

        foreach (var stuff in _stuffInScene)
        {
            if(stuff.GetComponent<Rigidbody>()) stuff.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void ObjectCollision(GameObject gObject, Collision collision)
    {
        if (collision.gameObject.CompareTag("Sol")) return;
        if(_stuffInScene.Contains(collision.gameObject))
        {
            var position = gObject.transform.position;
            
            gObject.transform.position = new Vector3(position.x, collision.collider.bounds.max.y,
                position.z);
        }
        
        else if (collision.gameObject.CompareTag("Outside"))
        {
            gObject.transform.position = gObject.GetComponent<ObjectBehaviour>().Socket.transform.position;
            
            Vector3 newPos = new Vector3(
                gObject.transform.position.x + Random.Range(_range.x, _range.y),
                gObject.transform.position.y,
                gObject.transform.position.z + Random.Range(_range.x, _range.y)
            );
        }
    }
}
