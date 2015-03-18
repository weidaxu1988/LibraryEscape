using UnityEngine;
using System.Collections;

public class PuzzleObject : MonoBehaviour
{

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
        if (activited) return;

        activited = true;
        tweenScale.PlayForward();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!activited) return;

        activited = false;
        tweenScale.PlayReverse();
    }

    void OnMouseUpAsButton()
    {
        if (activited)
            Debug.Log("down");
    }
}
