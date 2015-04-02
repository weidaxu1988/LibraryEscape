using UnityEngine;
using System.Collections;

public class ExitObject : MonoBehaviour
{
    private CircleCollider2D collider;
    private TweenScale tweenScale;

    private bool activited = false;

    void Awake()
    {
        tweenScale = GetComponentInChildren<TweenScale>();
        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;
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
            LevelControl.instance.OnExitObjectClick();
    }

    public void ActivateSelf()
    {
        collider.enabled = true;
        tweenScale.PlayForward();
    }
}
