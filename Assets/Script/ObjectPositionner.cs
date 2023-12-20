using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPositionner : MonoBehaviour
{
    [SerializeField] private RaycastBehaviour _raycast;
    [SerializeField] private GameObject _poofPrefabs;
    [SerializeField] private GameObject _pffPrefabs;
    public List<Objet> Objets;

    public static float Percentage = 0;
    public static UInt64 Milliseconds = 0;
    
    private IEnumerator ReplaceObjectCoRoutine(GameObject gameObject)
    {
        if(gameObject.CompareTag("Displaced")){
            foreach (Objet objet in Objets)
            {
                if(objet.pointer == gameObject)
                {
                    gameObject.tag = "Untagged";
                    if(gameObject.GetComponent<Rigidbody>()) gameObject.GetComponent<Rigidbody>().isKinematic = true;

                    GameObject pff = Instantiate(_pffPrefabs);
                    pff.transform.position = gameObject.transform.position;
                    var anim = gameObject.AddComponent<ScaleUpDown>();
                    anim.ScaleMax = 75;
                    anim.EaseInTime = 8;
                    anim.EaseOutTime = 15;
                    anim.Play();
                    
                    print("ntm");

                    while (anim.IsFinished != true)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    
                    print("ntm");

                    
                    gameObject.transform.position = objet.transform.position;
                    gameObject.transform.rotation = objet.transform.rotation;
                    gameObject.transform.localScale = objet.transform.localScale;

                    GameObject poof = Instantiate(_poofPrefabs);
                    poof.transform.position = objet.transform.position;

                    if (CheckWin())
                    {
                        CalculatePoints();
                        SceneManager.LoadScene("TheEnd");
                    }
                    
                    yield return null;
                }
            }
        }
        else if (gameObject.GetComponent<ObjectBehaviour>())
        {
            gameObject.GetComponent<ObjectBehaviour>().Exited = false;
            List<Color> color = new List<Color>();
            foreach(Material material in gameObject.GetComponent<Renderer>().materials){
                color.Add(material.color);
                material.color = new Color(1, 0, 0);
            }

            int countdown = 15;
            while (countdown >0)
            {
                if (gameObject.GetComponent<ObjectBehaviour>().Exited)
                {
                    foreach(Material material in gameObject.GetComponent<Renderer>().materials){
                        var index = gameObject.GetComponent<Renderer>().materials.ToList().IndexOf(material);
                        color[index] = (material.color);
                    }
                }
                
                foreach(Material material in gameObject.GetComponent<Renderer>().materials){
                    var index = gameObject.GetComponent<Renderer>().materials.ToList().IndexOf(material);
                    material.color += new Color((float)-((1-color[index].r)/15), (float)color[index].g/15, (float)color[index].b/15);
                }
                countdown -= 1;
                yield return new WaitForFixedUpdate();
            }
            
            foreach(Material material in gameObject.GetComponent<Renderer>().materials){
                var index = gameObject.GetComponent<Renderer>().materials.ToList().IndexOf(material);
                material.color = color[index];
            }
        }
    }

    private void ReplaceObject(GameObject objet)
    {
        StartCoroutine(ReplaceObjectCoRoutine(objet));
    }
    // Start is called before the first frame update
    void Start()
    {
        _raycast.OnHit += ReplaceObject;
    }

    public void TimerFinished()
    {
        CalculatePoints();
        SceneManager.LoadScene("TheEnd");
    }
    
    bool CheckWin()
    {
        foreach (Objet objet in Objets)
        {
            if (objet.pointer.CompareTag("Displaced"))
            {
                return false;
            }
        }

        return true;
    }

    void CalculatePoints()
    {
        int count = Objets.Count;
        foreach (Objet objet in Objets)
        {
            if (!objet.pointer.CompareTag("Displaced"))
            {
                Percentage += (float)100/count;
                print($"Object : {objet.pointer.name} is well placed -> Percentage = {Percentage}");
            }
            else
            {
                print($"Object : {objet.pointer.name} is not placed -> Percentage = {Percentage}");
            }
        }
    }
}
