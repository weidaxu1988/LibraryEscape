using UnityEngine;
using System.Collections;

public class ShortAnswerQ3 : ShortAnswerMultiKeyWords
{
    //protected override void HandleCorrectFeedback()
    //{
    //    string answer = inputs[0].value + " and " + inputs[1].value;
    //    feedbackLabel.text = "Good job! Negative emotions such as " + answer + " can be caused by library anxiety.";
    //}

    protected override void HandleIncorrectFeedback()
    {
        feedbackLabel.text = incorrectFeedBack[0];

        int[] results = new int[inputs.Length];


        int rCount = 0, inCount = 0;
        ArrayList list = new ArrayList(keyWords);
        for (int i = 0; i < inputs.Length; i++)
        {
            UIInput input = inputs[i];
            string answer = input.value;

            for (int j = 0; j < list.Count; j++)
            {
                string keyWord = (string)list[j];

                if (answer != null && answer.Contains(keyWord))
                {
                    results[i] = 1;
                    list.RemoveAt(i);
                    i--;
                    rCount++;
                    break;
                }
                else
                {
                    results[i] = 0;
                    inCount++;
                }
            }
        }

        if (rCount == 0)
            feedbackLabel.text = incorrectFeedBack[0];
        else if (rCount == 1)
            feedbackLabel.text = incorrectFeedBack[1];
        else if (rCount == 2)
            feedbackLabel.text = incorrectFeedBack[2];

        //string rAnswer = null, iAnswer = null;
        //for (int i = 0; i < results.Length;i++ )
        //{
        //    int r = results[i];
        //    if (r == 1)
        //    {
        //        rAnswer = inputs[i].value;
        //    }
        //    else
        //    {
        //        iAnswer = inputs[i].value;
        //    }
        //}

        //if (string.IsNullOrEmpty(rAnswer))
        //    feedbackLabel.text = "Library anxiety can give rise to negative feelings such as " + rAnswer + ", you’ve got this correct. Think about it again on " + iAnswer + ".";

        if (questionControl != null)
            questionControl.QuestionIncorrect();
    }
}
