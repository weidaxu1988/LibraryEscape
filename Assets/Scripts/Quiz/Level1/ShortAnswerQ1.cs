using UnityEngine;
using System.Collections;

public class ShortAnswerQ1 : ShortTextQuiz
{

    protected override void HandleIncorrectFeedback()
    {
        string result = input.value;

        feedbackLabel.text = incorrectFeedBack[0];
        if (!string.IsNullOrEmpty(result))
        {
            try
            {
                int year = int.Parse(result);
                if (year > 1974)
                {
                    feedbackLabel.text = incorrectFeedBack[1];
                }
                else
                {
                    feedbackLabel.text = incorrectFeedBack[0];
                }
            }
            catch (System.FormatException e)
            {
                
            }   
        }

        if (questionControl != null)
            questionControl.QuestionIncorrect();


    }
}
