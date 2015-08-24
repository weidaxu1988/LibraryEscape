using UnityEngine;
using System.Collections;

public class CompleteNoteControl : MonoBehaviour
{
    public GameObject[] stages;

    private int stageIndex;

    void Start()
    {
        InitStage();
    }

    private void InitStage()
    {
        stageIndex = 0;
        setPage(stageIndex);
        checkNavButtons();
    }

    public void nextPage()
    {
        stageIndex++;

        if (stageIndex >= stages.Length)
        {
            if (LevelControl.instance != null)
            {
                LevelControl.instance.LevelComplete();
            }
        }
        else
        {
            setPage(stageIndex);
            checkNavButtons();
        }
    }

    public void previousPage()
    {
        if (stageIndex > 0)
        {
            stageIndex--;
            setPage(stageIndex);
            checkNavButtons();
        }
    }

    private void setPage(int index)
    {

        if (index >= 0 && index < stages.Length)
        {
            GameObject stage = null;
            if (index > 0)
            {
                stage = stages[index - 1];
                if (stage.activeSelf)
                    stage.SetActive(false);
            }
            if (index < stages.Length - 1)
            {
                stage = stages[index + 1];
                if (stage.activeSelf)
                    stage.SetActive(false);

            }
            stage = stages[index];
            if (!stage.activeSelf)
                stage.SetActive(true);

            if (stage != null)
            {
                Animator libraianAnimator = GetComponentInChildren<Animator>();
                libraianAnimator.SetTrigger("clapping");
            }
        }
    }

    private void checkNavButtons()
    {
    }
}
