using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour {

    public void OnStartButtonClick()
    {
        GameManager.instance.loadManager.LoadStoryScene();
    }

    public void OnMoreButtonClick()
    {
        Debug.Log("Learn More Button Clicked");
    }
}
