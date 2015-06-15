using UnityEngine;
using System.Collections;

public class OptionFireKiller : MonoBehaviour {
    private TweenScale tweenScale;
    //private CircleCollider2D circleCollider;

    private AudioSource audioSource;

    void Awake()
    {
        //circleCollider = GetComponent<CircleCollider2D>();
        tweenScale = GetComponentInChildren<TweenScale>();
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerControl player = other.GetComponent<PlayerControl>();
            player.SetFireKillerActive(true);

            audioSource.Play();

            gameObject.SetActive(false);

            //tweenScale.PlayForward();
        }
    }

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        tweenScale.PlayReverse();
    //    }
    //}
}
