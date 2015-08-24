using UnityEngine;
using System.Collections;

public class GradeObject : MonoBehaviour
{

    public UILabel label;

    void OnEnable()
    {
        label.text = "By the way, I kept track of your performance, and your grade is " + GetGrade(GameManager.instance.player.FinalScore) + ". Congratulations again!";
    }

    static string GetGrade(int score)
    {
        string grade = "c+";

        if (score >= 260)
        {
            grade = "A+";
        }
        else if (score >= 222)
        {
            grade = "A";
        }
        else if (score >= 175)
        {
            grade = "B+";
        }
        else if (score >= 128)
        {
            grade = "B-";
        }

        return grade;
    }
}
