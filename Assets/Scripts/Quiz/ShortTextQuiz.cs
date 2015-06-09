using UnityEngine;
using System.Collections;

public class ShortTextQuiz : Quiz
{
    public string[] keyWords;
    public string correctFeedBack;
    public string[] incorrectFeedBack;
    public UILabel feedbackLabel;
    public UIInput[] inputs;


    public override void InitFeedback()
    {
        ShowFeedBack(true);

        finalScore = getScore();

        if (finalScore >= 1)
        {
            HandleCorrectFeedback();
            if (questionControl != null)
                questionControl.QuestionCorrect();
        }
        else
        {
            HandleIncorrectFeedback();
        }
    }

    protected override void HandleCorrectFeedback()
    {
        feedbackLabel.text = correctFeedBack;
    }

    public override void ClearResult()
    {
        foreach (UIInput input in inputs)
        {
            input.value = "";
        }
    }

    public override int getScore()
    {
        int result = 0;

        for (int i = 0; i < inputs.Length; i++)
        {
            UIInput input = inputs[i];
            string answer = input.value;
            string keyWord = keyWords[i];
            if (answer == null || !answer.Contains(keyWord))
            {
                result = 0;
                break;
            }
            else
            {
                result = 1;
            }
        }
        return result;
    }
}
