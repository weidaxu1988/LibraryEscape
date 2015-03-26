using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour
{
    public GameObject morePanel;

    public void OnStartButtonClick()
    {
        GameManager.instance.loadManager.LoadStoryScene();
    }

    public void OnMoreButtonClick()
    {
        if (morePanel.activeSelf)
        {
            morePanel.SetActive(false);
        }
        else
        {
            morePanel.SetActive(true);
        }
    }
}
