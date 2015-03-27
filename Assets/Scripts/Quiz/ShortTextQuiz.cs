using UnityEngine;
using System.Collections;

public class ShortTextQuiz : Quiz
{
    public string keyWord;

    private UIInput input;

    void Start()
    {
        input = GetComponentInChildren<UIInput>();
    }

    public override void ClearResult()
    {
        input.value = "";
    }

    public override int getScore()
    {
        int result = 0;
        
        string answer = input.value;
        if (answer != null && answer.Contains(keyWord))
        {
            result = 1;
        }
        
        return result;
    }
}
