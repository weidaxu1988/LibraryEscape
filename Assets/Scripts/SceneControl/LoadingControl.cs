using UnityEngine;
using System.Collections;

public class LoadingControl : MonoBehaviour
{
    public int waitTime = 2;

    void Start()
    {
        Invoke("LoadLevelScene", waitTime);
    }

    void LoadLevelScene()
    {
        GameManager.instance.loadManager.LoadLevelScene();
    }
}
