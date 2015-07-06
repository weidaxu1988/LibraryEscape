using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragDropQuiz : Quiz
{

    public string correctFeedBack;
    public string incorrectFeedBack;

    List<DragDropQuizContainer> questionList = new List<DragDropQuizContainer>();

    void Start()
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

    protected override void HandleCorrectFeedback()
    {
        feedbackLabel.text = correctFeedBack;
    }

    protected override void HandleIncorrectFeedback()
    {
        feedbackLabel.text = incorrectFeedBack;
    }
}
