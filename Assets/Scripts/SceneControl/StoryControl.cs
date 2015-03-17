using UnityEngine;
using System.Collections;

public class StoryControl : MonoBehaviour
{
    public GameObject[] stages;

    private int mCurStageIndex = 0;

    //void Start()
    //{
    //    SetupStage(0);
    //}

    public void SaveName(UILabel label)
    {
        string name = label.text;
        if (string.IsNullOrEmpty(name) || name.Equals(GameConfig.TXT_INPUT_DEFAULT) || name.Contains(GameConfig.TXT_WARNING_EMPTY))
            label.text = "name " + GameConfig.TXT_WARNING_EMPTY;
        else
        {
            if (GameManager.instance.player == null)
                GameManager.instance.player = new Player();
            else
                GameManager.instance.player.Name = name;

            NextStage();
        }
    }

    public void OnWriterFinished(GameObject button)
    {
        
    }

    public void SubmitQuestionOne(UILabel label)
    {
        SubmitQuestion(label, 1);
    }

    public void SubmitQuestionTwo(UILabel label)
    {
        SubmitQuestion(label, 2);
    }

    public void NextStage()
    {
        if (mCurStageIndex >= stages.Length - 1)
            GameManager.instance.loadManager.LoadLevelScene(1);
        else
            SetupStage(1);
    }

    public void PreviousStage()
    {
        SetupStage(-1);
    }

    private void SetupStage(int amount)
    {
        GameObject stage = stages[mCurStageIndex];
        stage.SetActive(false);

        mCurStageIndex += amount;
        stage = stages[mCurStageIndex];
        stage.SetActive(true);
    }

    private void SubmitQuestion(UILabel label, int index)
    {
        string content = label.text;
        if (string.IsNullOrEmpty(content) || content.Equals(GameConfig.TXT_INPUT_DEFAULT) || content.Contains(GameConfig.TXT_WARNING_EMPTY))
            label.text = "answer " + GameConfig.TXT_WARNING_EMPTY;
        else
        {
            GameManager.instance.player.SetAnswer(content, index);
            NextStage();
        }
    }
}
