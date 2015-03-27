using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrderQuiz : Quiz
{
    List<OrderQuizItem> questionList = new List<OrderQuizItem>();

    void Awake()
    {
        questionList.AddRange(GetComponentsInChildren<OrderQuizItem>());
    }

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
        return score / total;
    }
}
