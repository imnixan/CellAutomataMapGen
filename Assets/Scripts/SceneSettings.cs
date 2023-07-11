using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 300;
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
