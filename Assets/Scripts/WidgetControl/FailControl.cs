using UnityEngine;
using System.Collections;

public class FailControl : MonoBehaviour
{
    private Animator libraianAnimator;

    void OnEnable()
    {
        if (libraianAnimator == null)
        {
            libraianAnimator = GetComponentInChildren<Animator>();
        }
        if (libraianAnimator != null)
        {
            libraianAnimator.SetTrigger("concerned");
        }
    }

    public void LoadSelectScene()
    {
        GameManager.instance.SendEmail("type: level, failed level, index: " + LevelControl.instance.levelIndex);
        GameManager.instance.loadManager.LoadSelectScene();
    }
}
