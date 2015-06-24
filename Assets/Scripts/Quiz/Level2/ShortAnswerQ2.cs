using UnityEngine;
using System.Collections;

public class ShortAnswerQ2 : ShortAnswerMultiKeyWords
{
    protected override void HandleCorrectFeedback()
    {
        string answer = inputs[0].value + " and " + inputs[1].value;
        feedbackLabel.text = "Good job! Negative emotions such as " + answer + " can be caused by library anxiety.";
    }

    protected override void HandleIncorrectFeedback()
    {
        feedbackLabel.text = incorrectFeedBack[0];

        int[] results = new int[inputs.Length];

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
                    list.RemoveAt(j);
                    j--;
                    break;
                }
                else
                {
                    results[i] = 0;
                }
            }
        }

        string rAnswer = null, iAnswer = null;
        int count = 0;
        foreach (int i in results)
        {
            if (i == 1)
            {
                rAnswer = inputs[count].value;
            }
            else
            {
                iAnswer = inputs[count].value;
            }
            count++;
        }

        if (string.IsNullOrEmpty(rAnswer))
            feedbackLabel.text = "Library anxiety can give rise to negative feelings such as " + rAnswer + ", you’ve got this correct. Think about it again on " + iAnswer + ".";
        if (questionControl == null)
            questionControl = NGUITools.FindInParents<QuestionControl>(gameObject);
        if (questionControl != null)
            questionControl.QuestionIncorrect();
    }
}
