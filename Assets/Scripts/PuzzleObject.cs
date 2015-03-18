using UnityEngine;
using System.Collections;

public class PuzzleObject : MonoBehaviour {

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
        Debug.Log("enter 1");

        if (activited) return;
        Debug.Log("enter 2");
        activited = true;
        tweenScale.PlayForward();
    }

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    Debug.Log("stay");
    //}

    void OnTriggerExit2D(Collider2D other) {
        
        Debug.Log("exit 1");

        if (!activited) return;
        Debug.Log("exit 2");
        activited = false;
        tweenScale.PlayReverse();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
