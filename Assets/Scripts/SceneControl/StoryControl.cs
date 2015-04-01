using UnityEngine;
using System.Collections;

public class StoryControl : MonoBehaviour
{
    public GameObject[] stages;

    private int mCurStageIndex = 0;

    void Start()
    {
        SetupStage(0);
    }

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

    public void OnSkipButtonClick()
    {
        if (mCurStageIndex < 4)
        {
            SetupStage(4 - mCurStageIndex);
        }
    }

    public void SubmitQuestionOne(UILabel label)
    {
        SubmitQuestion(1, label);
    }

    public void SubmitQuestionTwo(UILabel label)
    {
        SubmitQuestion(2, label);
    }

    public void SubmitQuestionThree(UILabel l1, UILabel l2, UILabel l3)
    {
        SubmitQuestion(3, l1, l2, l3);
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

    public void OnFinalButtonActive(GameObject obj)
    {
        BoxCollider collider = obj.GetComponent<BoxCollider>();
        collider.enabled = true;
    }

    private void SetupStage(int amount)
    {
        GameObject stage = stages[mCurStageIndex];
        stage.SetActive(false);

        mCurStageIndex += amount;
        stage = stages[mCurStageIndex];
        stage.SetActive(true);
    }

    private void SubmitQuestion(int index, params UILabel[] label)
    {
        bool result = false;

        string[] contents = new string[label.Length];
        for (int i = 0; i < label.Length; i++)
        {
            UILabel l = label[i];
            string content = l.text;
            if (string.IsNullOrEmpty(content) || content.Equals(GameConfig.TXT_INPUT_DEFAULT) || content.Contains(GameConfig.TXT_WARNING_EMPTY) || content.Contains(GameConfig.TXT_INPUT_ANSWER_DEFAULT))
                l.text = "answer " + GameConfig.TXT_WARNING_EMPTY;
            else
                contents[i] = content;
        }


        if (result)
        {
            GameManager.instance.player.SetAnswer(contents, index);
            NextStage();
        }
    }
}
