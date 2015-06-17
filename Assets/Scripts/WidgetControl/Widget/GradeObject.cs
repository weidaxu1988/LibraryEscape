using UnityEngine;
using System.Collections;

public class GradeObject : MonoBehaviour
{

    public UILabel label;

    void OnEnable()
    {
        label.text = "By the way, I kept track of your performance, and your grade is " + GameManager.instance.player.FinalScore + ". Congratulations again!";
    }
}
