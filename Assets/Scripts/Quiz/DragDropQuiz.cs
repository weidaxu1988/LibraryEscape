using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragDropQuiz : Quiz
{

    public string correctFeedBack;
    public string incorrectFeedBack;

    public UILabel feedbackLabel;

    List<DragDropQuizContainer> questionList = new List<DragDropQuizContainer>();

    void Awake()
    {
        questionList.AddRange(GetComponentsInChildren<DragDropQuizContainer>());
    }

    public override void ClearResult()
    {
        foreach (DragDropQuizContainer c in questionList)
        {
            c.ReleaseItem();
        }
    }

    public override void SecondReset()
    {
        ShowFeedBack(false);

        foreach (DragDropQuizContainer c in questionList)
        {
            c.ReleaseFalseItem();
        }
    }

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
            feedbackLabel.text = incorrectFeedBack;
            if (questionControl != null)
                questionControl.QuestionIncorrect();
        }
    }

    public override int getScore()
    {
        int total = questionList.Count;
        int score = 0;
        foreach (DragDropQuizContainer c in questionList)
        {
            if (c.getResult())
                score++;
        }
        return score / total;
    }
}
