using UnityEngine;
using System.Collections;

public class FailControl : MonoBehaviour {
    public void LoadSelectScene()
    {
        GameManager.instance.SendEmail("type: level, failed level, index: " + LevelControl.instance.levelIndex);
        GameManager.instance.loadManager.LoadSelectScene();
    }
}
