using UnityEngine;
using System.Collections;

public class MultiChoiceQuiz : Quiz
{
    public UIToggle correctOption;

    private UIToggle[] optionArray;

    void Start()
    {
        optionArray = GetComponentsInChildren<UIToggle>();
    }

    public override int getScore()
    {
        int result = 0;

        foreach (UIToggle opt in optionArray)
        {
            if (opt.value)
            {
                if (opt == correctOption)
                {
                    result++;
                    break;
                }
            }
        }

        return result;
    }
}
