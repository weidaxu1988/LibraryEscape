using UnityEngine;
using System.Collections;

public class FireObject : MonoBehaviour
{
    const float delayTime = 1;
    const float endDelayTime = 10;

    private TweenScale tweenScale;
    //private CircleCollider2D circleCollider;

    private AudioSource audioSource;

	public GameObject block;
    public GameObject fileKiller;

    private bool activited = false;
    private bool fireActived = false;

    private float timeCount;
    private float endCount;

    void Awake()
    {
        //circleCollider = GetComponent<CircleCollider2D>();
        tweenScale = GetComponentInChildren<TweenScale>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (LevelControl.instance.isGamePaused)
            return;

        if (activited)
        {
            if (timeCount < delayTime)
            {
                timeCount += Time.deltaTime;
            }
            else
            {
                FireEnd();
            }
        }
        if (fireActived)
        {
            if (endCount < endDelayTime)
            {
                endCount += Time.deltaTime;
            }
            else
            {
                fireActived = false;
                activited = false;
                LevelControl.instance.GameFailed();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!fireActived && other.tag == "Ghost")
        {
            FireStart();
        }

        if (fireActived && other.tag == "Player")
        {
            PlayerControl player = other.GetComponent<PlayerControl>();
            
            if (!player.HasFireKiller()) return;
            
            if (activited) return;

            fileKiller.SetActive(true);

            audioSource.Play();
            
//disable click open automatically
            activited = true;
            //LevelControl.instance.OnPuzzleObjectClick(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (fireActived && other.tag == "Player")
        {
            if (!activited) return;

            fileKiller.SetActive(false);

            activited = false;
            
            timeCount = 0;
        }
    }

    void FireStart()
    {
        fireActived = true;
        tweenScale.PlayForward();
        audioSource.Play();
		block.SetActive (true);
    }
         
    void FireEnd()
    {
        fileKiller.SetActive(false);

        fireActived = false;
        activited = false;
        timeCount = 0;
        endCount = 0;
        tweenScale.PlayReverse();
		block.SetActive (false);
    }

}
