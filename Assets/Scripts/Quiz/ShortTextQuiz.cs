using UnityEngine;
using System.Collections;

public class ShortTextQuiz : Quiz
{
    public string keyWord;
    public string correctFeedBack;
    public string[] incorrectFeedBack;
    public UILabel feedbackLabel;
    public UIInput input;


    public override void InitFeedback()
    {
        ShowFeedBack(true);

        finalScore = getScore();

        if (finalScore >= 1)
        {
            feedbackLabel.text = correctFeedBack;
            if (questionControl != null)
                questionControl.QuestionCorrect();
        }
        else
        {
            HandleIncorrectFeedback();
        }
    }

    public override void ClearResult()
    {
        if (input)
            input.value = "";
    }

    public override int getScore()
    {
        int result = 0;

        string answer = input.value;
        if (answer != null && answer.Contains(keyWord))
        {
            result = 1;
        }

        return result;
    }
}
