using UnityEngine;
using System.Collections;

public class ShortAnswerMultiKeyWords : ShortTextQuiz {

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
                    list.RemoveAt(i);
                    i--;
                    break;
                }
                else
                {
                    results[i] = 0;
                }
            }
        }

        foreach (int i in results)
        {
            if (i == 1)
            {
                feedbackLabel.text = incorrectFeedBack[1];
                break;
            }
        }

        if (questionControl != null)
            questionControl.QuestionIncorrect();


    }

    public override int getScore()
    {
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
                    list.RemoveAt(i);
                    i--;
                    break;
                }
                else
                {
                    results[i] = 0;   
                }
            }

                
        }

        foreach (int i in results) {
            if (i != 1)
            {
                return 0;
            }
        }

        return 1;
    }
}
