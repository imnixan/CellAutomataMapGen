using UnityEngine;
using System.Collections;
using TMPro;

public class ShowFPS : MonoBehaviour
{
    private TextMeshProUGUI counter;
    public static int fps;

    private void Start()
    {
        counter = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FpsShower());
    }

    private void Update()
    {
        fps = (int)(1.0f / Time.deltaTime);
    }

    private IEnumerator FpsShower()
    {
        while (true)
        {
            counter.text = fps.ToString();
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
