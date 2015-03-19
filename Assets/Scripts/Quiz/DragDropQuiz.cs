using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragDropQuiz : Quiz {

    List<DragDropQuizContainer> questionList = new List<DragDropQuizContainer>();

    void Awake()
    {
        questionList.AddRange(GetComponentsInChildren<DragDropQuizContainer>());
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
