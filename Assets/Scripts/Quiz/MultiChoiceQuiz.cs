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
