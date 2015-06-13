using UnityEngine;
using System.Collections;

public class FailControl : MonoBehaviour {
    public void LoadSelectScene()
    {
        GameManager.instance.loadManager.LoadSelectScene();
    }
}
