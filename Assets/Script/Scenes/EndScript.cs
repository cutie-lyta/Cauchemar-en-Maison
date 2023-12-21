using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputBox;

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        scoreText.text = $"Score : {ObjectPositionner.Percentage}%";
        
        TimeSpan ts = TimeSpan.FromMilliseconds(ObjectPositionner.Milliseconds);
        timeText.text = $"Time : {ts.ToString(@"mm\:ss\.ffff")}";
    }

    public void OnPushToServer()
    {
        print(inputBox.text);
        StartCoroutine(Server.UploadScore(inputBox.text, ObjectPositionner.Percentage, ObjectPositionner.Milliseconds));
    }

    public void OnDrawLeaderboard()
    {
        Action<List<Server.Score>> @event = null;
        @event += (list =>
        {
            panel.SetActive(true);
            
            var child = panel.transform.GetChild(0);

            var namesList = child.transform.GetChild(0).GetComponentsInChildren<TMP_Text>();
            var scoresList = child.transform.GetChild(1).GetComponentsInChildren<TMP_Text>();
            var timerList = child.transform.GetChild(2).GetComponentsInChildren<TMP_Text>();
            
            print(child.name);
            
            print(namesList.Length);
            print(scoresList.Length);
            print(timerList.Length);

            list.Sort((score, score1) =>
            {
                var ret = -score.percentage.CompareTo(score1.percentage);
                if (ret == 0) ret = score.ms.CompareTo(score1.ms);
                return ret;
            });
            
            for (int i = 0; i < 11; i++)
            {
                if ((i+1)>list.Count)
                {
                    namesList[i].text = "";
                    scoresList[i].text = "";
                    timerList[i].text = "";
                    continue;
                }

                var name = list[i].name;
                namesList[i].text = name;
                scoresList[i].text = list[i].percentage + "%";
                TimeSpan ts = TimeSpan.FromMilliseconds(list[i].ms);
                timerList[i].text = ts.ToString(@"mm\:ss\.ffff");
            }
        });

        StartCoroutine(Server.GetScoreOfAll(@event));
    }

    public void OnAskLeaderboard(String str)
    {
        Action<Server.Score> @event = null;
        @event += (score =>
        {
            var child = panel.transform.GetChild(0);

            var names = child.transform.GetChild(0).GetComponentsInChildren<TextMeshProUGUI>()[10];
            var scores = child.transform.GetChild(1).GetComponentsInChildren<TextMeshProUGUI>()[10];
            var timer = child.transform.GetChild(2).GetComponentsInChildren<TextMeshProUGUI>()[10];

            names.text = score.name;
            scores.text = score.percentage + "%";
            TimeSpan ts = TimeSpan.FromMilliseconds(score.ms);
            timer.text = ts.ToString(@"mm\:ss\.ffff");
        });
        StartCoroutine(Server.GetScore(str, @event));
    }
    public void OnHideLeaderboard()
    {
        panel.SetActive(false);
    }
    
}
