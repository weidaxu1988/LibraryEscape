using UnityEngine;
using System.Collections;

public class SelectControl : MonoBehaviour
{
    public GameObject dialog;

    public void OnLevelOneClick()
    {
        LoadLevel(1);
    }

    public void OnLevelTwoClick()
    {
        //ShowDialog();
        LoadLevel(2);
    }

    public void OnLevelThreeClick()
    {
        //ShowDialog();
        LoadLevel(3);
    }

    public void OnLevelFourClick()
    {
        //ShowDialog();
        LoadLevel(4);
    }

    public void OnLevelFiveClick()
    {
        //ShowDialog();
        LoadLevel(5);
    }

    public void OnLevelSixClick()
    {
        //ShowDialog();
        LoadLevel(6);
    }

    public void ShowDialog()
    {
        if (!dialog.activeSelf)
        {
            dialog.SetActive(true);
        }
    }

    public void OnDialogHideButtonClick()
    {
        if (dialog.activeSelf)
        {
            dialog.SetActive(false);
        }
    }

    private void LoadLevel(int level)
    {
        if (GameManager.instance.CurrentLevel == level)
            GameManager.instance.loadManager.LoadLevelSceneDirectly(level);
    }
}
