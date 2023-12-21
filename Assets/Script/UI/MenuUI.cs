using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private GameObject _panelCredits;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _returnButton;

    // Start is called before the first frame update
    void Start()
    {
        GoToMenu();
    }

    public void GoToMenu()
    {
        _panelMenu.SetActive(true);
        _panelCredits.SetActive(false);

        _startButton.Select();
    }

    public void GoToCredits()
    {
        _panelMenu.SetActive(false);
        _panelCredits.SetActive(true);

        _returnButton.Select();
    }
}
