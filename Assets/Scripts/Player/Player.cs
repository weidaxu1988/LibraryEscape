using UnityEngine;
using System.Collections;

public class Player
{

    private int finalScore;
    public int FinalScore
    {
        get
        {
            return finalScore;
        }
    }

    private string name;
    public string Name { get { return name; } set { name = value; } }

    public void SetAnswer(string[] content, int index)
    {
        
    }

    public void AddTotalScore(int failCount)
    {
        if (failCount == 0)
        {
            Debug.Log("+++++++++++++++10, total: " + finalScore);
            finalScore += 10;
        }
        else if (failCount == 1)
        {
            Debug.Log("+++++++++++++++5, total: " + finalScore);
            finalScore += 5;
        }
        else
        {
            Debug.Log("+++++++++++++++3, total: " + finalScore);
            finalScore += 3;
        }
    }
}
