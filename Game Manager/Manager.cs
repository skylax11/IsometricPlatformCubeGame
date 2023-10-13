using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    private void Start()
    {
        Time.timeScale = 0f;
    }
    public void StartGame()
    {
        _panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
