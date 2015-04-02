using UnityEngine;
using System.Collections;

public class MultiChoiceQuiz : Quiz
{
    public UIToggle correctOption;

    private UIToggle[] optionArray;

    void Start()
    {
        optionArray = GetComponentsInChildren<UIToggle>();
    }

    public override void InitFeedback()
    {
        ShowFeedBack(true);

        finalScore = getScore();

        if (finalScore >= 1)
        {
            if (questionControl != null)
                questionControl.QuestionCorrect();
        }
        else
        {
            if (questionControl != null)
                questionControl.QuestionIncorrect();
        }
    }

    public override void ClearResult()
    {
        foreach (UIToggle opt in optionArray)
        {
            opt.value = false;
        }
    }

    public override int getScore()
    {
        UILabel feedbackLabel = feedbackContainer.GetComponentInChildren<UILabel>();

        int result = 0;

        foreach (UIToggle opt in optionArray)
        {
            if (opt.value)
            {
                feedbackLabel.text = opt.GetComponent<QuizItemFeedback>().feedback;

                if (opt == correctOption)
                {
                    result++;
                    break;
                }
            }
        }

        return result;
    }
}
