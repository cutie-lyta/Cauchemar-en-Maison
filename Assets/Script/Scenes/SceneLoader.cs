using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    [SerializeField] private AudioClip serverFailed;
    [SerializeField] private AudioClip serverSuccess;

    public void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }
    public void LoadTheScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void PlayAudioServer(bool failed)
    {
        var i = GetComponent<AudioSource>();
        if (!i)
        {
            i = gameObject.AddComponent<AudioSource>();
        }

        i.clip = failed ? serverFailed : serverSuccess;
        i.Play();
    }
}
