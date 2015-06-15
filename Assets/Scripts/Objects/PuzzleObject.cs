using UnityEngine;
using System.Collections;

public class PuzzleObject : MonoBehaviour
{
    const float delayTime = 1;

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
    private bool showed = false;
    private float timeCount;

    void Awake()
    {
        //circleCollider = GetComponent<CircleCollider2D>();
        tweenScale = GetComponentInChildren<TweenScale>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (activited && !showed)
        {
            if (timeCount < delayTime)
            {
                timeCount += Time.deltaTime;
            }
            else
            {
                showed = true;
                LevelControl.instance.OnPuzzleObjectClick(this);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activited) return;

            audioSource.Play();
            
//disable click open automatically
            activited = true;
            //LevelControl.instance.OnPuzzleObjectClick(this);


            tweenScale.PlayForward();

            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!activited) return;

            activited = false;
            showed = false;
            timeCount = 0;
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
