using UnityEngine;
using System.Collections;

public class PuzzleObject : MonoBehaviour
{
    public enum StartType
    {
        Purple,
        Green,
        Orange
    }

    public StartType startType;

    public string noteTitle;
    public string noteContent;

    private TweenScale tweenScale;
    private CircleCollider2D circleCollider;

    private bool activited = false;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        tweenScale = GetComponentInChildren<TweenScale>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activited) return;

            activited = true;
            tweenScale.PlayForward();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!activited) return;

            activited = false;
            tweenScale.PlayReverse();
        }
    }

    void OnMouseUpAsButton()
    {
        if (activited)
            LevelControl.instance.OnPuzzleObjectClick(this);
    }

    public bool checkAnswer(string answer)
    {
        bool result = false;

        return result;
    }
}
