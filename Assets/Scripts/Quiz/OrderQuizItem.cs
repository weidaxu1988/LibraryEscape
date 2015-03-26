using UnityEngine;
using System.Collections;

public class OrderQuizItem : MonoBehaviour
{
    public int order;

    private UIInput input;

    void Awake()
    {
        input = GetComponentInChildren<UIInput>();
    }

    public void OnOrderInput()
    {
        string str = input.value;
        char c = str[0];

        if (str.Length > 1)
        {
            input.value = c.ToString();
        }

        if (c != '1' && c != '2' && c != '3' && c != '4' && c != '5' && c != '6' && c != '7' && c != '8' && c != '9')
        {
            input.value = "";
        }
    }

    public bool getResult()
    {
        bool result = false;
        string content = input.value;
        if (string.IsNullOrEmpty(content))
        {
            int ord = int.Parse(content);
            if (ord == order)
            {
                result = true;
            }
        }
        return result;
    }
}
