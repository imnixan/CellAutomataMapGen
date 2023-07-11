using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]
    Image soundBtn,
        vibroBtn;

    [SerializeField]
    Sprite[] switcherSprites;

    private void Start()
    {
        SetIcons();
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ChangeSound()
    {
        ChangeSettings("Sound");
    }

    public void ChangeVibro()
    {
        ChangeSettings("Vibro");
    }

    private void ChangeSettings(string setting)
    {
        PlayerPrefs.SetInt(setting, PlayerPrefs.GetInt(setting, 1) == 1 ? 0 : 1);
        PlayerPrefs.Save();
        SetIcons();
    }

    private void SetIcons()
    {
        soundBtn.sprite = switcherSprites[PlayerPrefs.GetInt("Sound", 1)];
        vibroBtn.sprite = switcherSprites[PlayerPrefs.GetInt("Vibro", 1)];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }
}
