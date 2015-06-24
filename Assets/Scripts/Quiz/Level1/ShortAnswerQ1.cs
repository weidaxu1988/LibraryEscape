using UnityEngine;
using System.Collections;

public class ShortAnswerQ1 : ShortTextQuiz
{

    protected override void HandleIncorrectFeedback()
    {
        string result = inputs[0].value;

        feedbackLabel.text = incorrectFeedBack[1];
        if (!string.IsNullOrEmpty(result))
        {
            try
            {
                int year = int.Parse(result);
                if (year >= 1970 && year <= 1979)
                {
                    feedbackLabel.text = incorrectFeedBack[0];
                }
            }
            catch (System.FormatException e)
            {
                
            }   
        }
        if (questionControl == null)
            questionControl = NGUITools.FindInParents<QuestionControl>(gameObject);
        if (questionControl != null)
            questionControl.QuestionIncorrect();


    }
}
