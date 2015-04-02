using UnityEngine;
using System.Collections;

public class ShortTextQuiz : Quiz
{
    public string keyWord;
    public string correctFeedBack;
    public string incorrectFeedBack;
    private UIInput input;

    void Start()
    {
        input = GetComponentInChildren<UIInput>();
    }

    public override void InitFeedback()
    {
        ShowFeedBack(true);

        UILabel feedbackLabel = feedbackContainer.GetComponentInChildren<UILabel>();
        
        finalScore = getScore();

        if (finalScore >= 1)
        {
            feedbackLabel.text = correctFeedBack;
            if (questionControl != null)
                questionControl.QuestionCorrect();
        }
        else
        {
            feedbackLabel.text = incorrectFeedBack;
            if (questionControl != null)
                questionControl.QuestionIncorrect();
        }
    }

    public override void ClearResult()
    {
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
