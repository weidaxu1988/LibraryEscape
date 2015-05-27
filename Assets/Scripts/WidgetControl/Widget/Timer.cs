using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public int maxMinutes;

    private UILabel label;

    private float toSeconds;

    void Start()
    {
        label = GetComponent<UILabel>();
        toSeconds = maxMinutes * 60;
    }

    void Update()
    {
        if (!LevelControl.instance.isGamePaused)
        {
            toSeconds -= Time.deltaTime;
            if (toSeconds >= 0)
            {
                label.text = GetTime(toSeconds);
            }
            else
            {
                LevelControl.instance.CountDownFinished();
            }
        }
    }

    public static string GetTime(float remaining)
    {
        int minutes = (int)(remaining / 60);
        int seconds = (int)(remaining % 60);
         
        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
