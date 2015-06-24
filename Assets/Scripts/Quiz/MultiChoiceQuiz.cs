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
            GameManager.instance.player.AddTotalScore(failCount);

            HandleCorrectFeedback();

            Debug.Log("position check");

            if (questionControl == null)
                questionControl = NGUITools.FindInParents<QuestionControl>(gameObject);

            if (questionControl != null) {
                Debug.Log("question not null");
                questionControl.QuestionCorrect();
            }
            else
            {
                Debug.Log("question null");
            }
        }
        else
        {
            
            failCount++;
     
            HandleIncorrectFeedback();
            if (questionControl == null)
                questionControl = NGUITools.FindInParents<QuestionControl>(gameObject);

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
