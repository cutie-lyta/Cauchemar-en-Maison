using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Newtonsoft.Json;

public static class Server
{
    public struct Score
    {
        public string name;
        public float percentage;
        public ulong ms;
    }

    public struct JSONScore
    {
        public string name;
        public string percentage;
        public string ms;
    }

    public static IEnumerator UploadScore(string nom, float percentage, ulong ms)
    {
        if (nom == "")
        {
            yield return null;
        }

        WWWForm form = new WWWForm();
        form.AddField("name", nom);
        form.AddField("score", percentage.ToString());
        form.AddField("time", ms.ToString());
        Debug.Log($"name={nom}&score={percentage}&time={ms}");

        UnityWebRequest www =
            UnityWebRequest.Post("https://cauchemarenmaison.000webhostapp.com/api/setNewScore.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    public static IEnumerator GetScore(string nom, Action<Score> callBack)
    {
        using (UnityWebRequest webRequest =
               UnityWebRequest.Get($"https://cauchemarenmaison.000webhostapp.com/api/getScore.php?name={nom}"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            Score score = new Score();
            score.name = nom;
            score.percentage = 0;
            score.ms = 0;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    break;
                case UnityWebRequest.Result.Success:
                    var output = webRequest.downloadHandler.text.Split("|");

                    Debug.Log($"{output[0]}");
                    score.percentage = float.Parse(output[0]);
                    score.ms = UInt64.Parse(output[1]);
                    break;
            }

            callBack?.Invoke(score);
        }
    }

    public static IEnumerator GetScoreOfAll(Action<List<Score>> callBack)
    {
        try
        {
            using (UnityWebRequest webRequest =
                   UnityWebRequest.Get("https://cauchemarenmaison.000webhostapp.com/api/getAllScore.php"))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                List<Score> scores = new();

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                    case UnityWebRequest.Result.ProtocolError:
                        GameObject.FindObjectOfType<SceneLoader>().PlayAudioServer(true);
                        break;
                    case UnityWebRequest.Result.Success:
                        Debug.Log($"AAAAAAAAAAAAAAAAAAAA : {webRequest.downloadHandler.text}");
                        scores = JsonConvert.DeserializeObject<List<Score>>(webRequest.downloadHandler.text);
                        GameObject.FindObjectOfType<SceneLoader>().PlayAudioServer(false);

                        //List<JSONScore> jsonScores = JsonConvert.DeserializeObject<List<JSONScore>>(webRequest.downloadHandler.text);
                        /*foreach (JSONScore jscore in jsonScores)
                        {
                            Score score;
                            score.name = jscore.name;
                            score.percentage = float.Parse(jscore.percentage);
                            score.ms = UInt64.Parse(jscore.ms);
                        }*/
                        break;
                }

                if (scores is not null) callBack?.Invoke(scores);
            }
        }
        catch (Exception e)
        {
            GameObject.FindObjectOfType<SceneLoader>().PlayAudioServer(true);
        }
    }
}
