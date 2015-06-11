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

    public PuzzleObject previousPuzzle;

    public string noteTitle;
    public string[] noteContentArray;

    private TweenScale tweenScale;
    //private CircleCollider2D circleCollider;

    private AudioSource audioSource;

    private bool activited = false;

    void Awake()
    {
        //circleCollider = GetComponent<CircleCollider2D>();
        tweenScale = GetComponentInChildren<TweenScale>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activited) return;

            audioSource.Play();
            
//disable click open automatically
            //activited = true;
            LevelControl.instance.OnPuzzleObjectClick(this);


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
