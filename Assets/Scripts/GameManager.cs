using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject gameCanvas,
        startCanvas,
        endCanvas;

    [SerializeField]
    SpriteRenderer bg;

    [SerializeField]
    Movement player;

    [SerializeField]
    GameObject tankMission,
        gunMission,
        carMission;

    [SerializeField]
    Button startButton;

    [SerializeField]
    TextMeshProUGUI endText;
    private int tanksLeft,
        carsLeft,
        gunsLeft;

    private void Awake()
    {
        gameCanvas.SetActive(false);
        startCanvas.SetActive(true);
        endCanvas.SetActive(false);
        GenerateMission();
        startButton.interactable = false;
    }

    public void StartGame()
    {
        startButton.interactable = true;
    }

    public void StartMission()
    {
        player.Init();
        gameCanvas.SetActive(true);
        startCanvas.SetActive(false);
        StartCoroutine(HideBg());
    }

    private void GenerateMission()
    {
        System.Random randomGenerator = new System.Random(PlayerPrefs.GetInt("Seed"));
        gunsLeft = randomGenerator.Next(1, 10);
        tanksLeft = randomGenerator.Next(0, 5);
        carsLeft = randomGenerator.Next(0, 7);
        SetMission(gunMission, gunsLeft);
        SetMission(tankMission, tanksLeft);
        SetMission(carMission, carsLeft);
    }

    private void SetMission(GameObject missonType, int count)
    {
        if (count > 0)
        {
            missonType.GetComponentInChildren<TextMeshProUGUI>().text = count.ToString();
        }
        else
        {
            missonType.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Enemy.EnemyDestroy += OnEnemyDestroy;
        Movement.PlayerDestroy += OnPlayerDestroy;
    }

    private void OnDisable()
    {
        Enemy.EnemyDestroy -= OnEnemyDestroy;
        Movement.PlayerDestroy -= OnPlayerDestroy;
    }

    void OnPlayerDestroy(Vector3 pos)
    {
        EndGame(false);
    }

    void OnEnemyDestroy(Enemy type, Vector3 pos, bool playerKill)
    {
        if (playerKill)
        {
            switch (type)
            {
                case Tank:
                    tanksLeft--;
                    break;
                case Car:
                    carsLeft--;
                    break;
                case Gun:
                    gunsLeft--;
                    break;
            }
            CheckMission();
        }
    }

    private void EndGame(bool win)
    {
        gameCanvas.SetActive(false);
        endCanvas.SetActive(true);
        endText.text = win ? "Миссия выполнена!" : "Миссия провалена";
    }

    private void CheckMission()
    {
        Debug.Log($"Check Mission cars {carsLeft}, guns {gunsLeft}, tanksleft {tanksLeft}");
        if (tanksLeft <= 0 && carsLeft <= 0 && gunsLeft <= 0)
        {
            EndGame(true);
        }
    }

    private IEnumerator HideBg()
    {
        for (float a = 1; a > 0; a -= 0.1f)
        {
            bg.color = new Color(1, 1, 1, a);
            yield return new WaitForSeconds(0.05f);
        }
        bg.color = new Color(1, 1, 1, 0);
    }
}
