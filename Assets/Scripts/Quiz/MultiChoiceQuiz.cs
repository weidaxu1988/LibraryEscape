using UnityEngine;
using System.Collections;

public class MultiChoiceQuiz : Quiz
{
    public UILabel feedbackLabel;

    public UIToggle correctOption;

    private UIToggle[] optionArray;

    void Awake()
    {
        optionArray = GetComponentsInChildren<UIToggle>();
    }

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
            if (questionControl != null)
                questionControl.QuestionIncorrect();
        }
    }

    public override void ClearResult()
    {
        if (optionArray != null && optionArray.Length > 0)
            foreach (UIToggle opt in optionArray)
            {
                opt.value = false;
            }
    }

    public override int getScore()
    {
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
