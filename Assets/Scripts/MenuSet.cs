using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuSet : MonoBehaviour
{
    private TMP_InputField inputField;

    void Start()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        inputField.text = PlayerPrefs.GetInt("Seed", 1337).ToString();
    }

    public void StartGame()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            GenerateSeed();
        }
        PlayerPrefs.SetInt("Seed", int.Parse(inputField.text));
        SceneManager.LoadScene("Game");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GenerateSeed()
    {
        inputField.text = Random.Range(0, 10000).ToString();
    }
}
