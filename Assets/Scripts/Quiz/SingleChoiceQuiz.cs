using UnityEngine;
using System.Collections;

public class SingleChoiceQuiz : MultiChoiceQuiz {

    public string correctFeedback;
    public string incorrectFeedback;

    public UIToggle[] correctOptions;

    protected override void HandleCorrectFeedback()
    {
        feedbackLabel.text = correctFeedback;
    }

    protected override void HandleIncorrectFeedback()
    {
        int result = 0;

        foreach (UIToggle opt in correctOptions)
        {
            if (opt.value)
                result++;
        }

        if (result < correctOptions.Length)
            feedbackLabel.text = incorrectFeedback;
    }

    public override int getScore()
    {
        int result = 0;

        foreach (UIToggle opt in correctOptions)
        {
            if (opt.value)
                result++;
        }

        return result == correctOptions.Length ? 1 : 0;
    } 

}
