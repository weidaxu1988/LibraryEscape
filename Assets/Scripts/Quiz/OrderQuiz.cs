﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrderQuiz : Quiz
{
    List<OrderQuizItem> questionList = new List<OrderQuizItem>();

    void Start()
    {
        questionList.AddRange(GetComponentsInChildren<OrderQuizItem>());
    }

    //public override void InitFeedback()
    //{
    //    ShowFeedBack(true);

    //    finalScore = getScore();

    //    if (finalScore >= 1)
    //    {
    //        GameManager.instance.player.AddTotalScore(failCount);

    //        if (questionControl == null)
    //            questionControl = NGUITools.FindInParents<QuestionControl>(gameObject);

    //        if (questionControl != null)
    //            questionControl.QuestionCorrect();
    //    }
    //    else
    //    {
    //        failCount++;

    //        if (questionControl == null)
    //            questionControl = NGUITools.FindInParents<QuestionControl>(gameObject);

    //        if (questionControl != null)
    //            questionControl.QuestionIncorrect();
    //    }
    //}

    public override void ClearResult()
    {
        foreach (OrderQuizItem c in questionList)
        {
            c.ClearOrder();
        }
    }

    public override int getScore()
    {
        int total = questionList.Count;
        int score = 0;
        foreach (OrderQuizItem c in questionList)
        {
            if (c.getResult())
                score++;
        }
        Debug.Log("score"+score);
        return score / total;
    }
}
