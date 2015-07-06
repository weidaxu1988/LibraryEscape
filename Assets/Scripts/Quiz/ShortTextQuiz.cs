using UnityEngine;
using System.Collections;
using System;

public class ShortTextQuiz : Quiz
{
    public string[] keyWords;
    public string correctFeedBack;
    public string[] incorrectFeedBack;
    public UIInput[] inputs;


    //public override void InitFeedback()
    //{
    //    ShowFeedBack(true);

    //    finalScore = getScore();

    //    if (finalScore >= 1)
    //    {
    //        GameManager.instance.player.AddTotalScore(failCount);
    //        HandleCorrectFeedback();
    //        questionControl.QuestionCorrect();
    //    }
    //    else
    //    {
    //        failCount++;
    //        HandleIncorrectFeedback();
    //        questionControl.QuestionIncorrect();
    //    }
    //}

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
            if (answer == null || !(answer.IndexOf(keyWord, StringComparison.InvariantCultureIgnoreCase) >= 0))
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
