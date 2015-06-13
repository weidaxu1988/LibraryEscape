using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public bool timeSensitive;
    public int sleepTime;
    public int awakeTime;

    public Vector3 startPos;
    public Vector3 endPos;

    public bool enemyActive;

    private float timeCount;

    private Vector3 oriScale;

    void Start()
    {
        oriScale = transform.localScale;
    }

    void Update()
    {
        if (!LevelControl.instance.isGamePaused)
        {
            if (timeSensitive)
            {
                TimeProcess();
            }
            else
            {
                MoveProcess();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //audioSource.Play();

            ////disable click open automatically
            ////activited = true;
            //LevelControl.instance.OnPuzzleObjectClick(this);


            //tweenScale.PlayForward();

            GhostCaught();
        }
    }

    void GhostCaught()
    {
        LevelControl.instance.GameFailed();
        gameObject.SetActive(false);
    }

    void TimeProcess()
    {
        if (!enemyActive)
        {
            if (timeCount < sleepTime)
            {
                timeCount += Time.deltaTime;
            }
            else
            {
                enemyActive = true;
                timeCount = 0;
            }
        }
        else
        {
            if (timeCount < awakeTime)
            {
                //float speed = 1 / awakeTime / 2;
                timeCount += Time.deltaTime;
                
                if (timeCount < awakeTime / 2)
                {
                    Vector3 scale = oriScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                    transform.position = Vector3.Lerp(startPos, endPos, timeCount / (awakeTime / 2));
                }
                else
                {
                    transform.localScale = oriScale;
                    transform.position = Vector3.Lerp(endPos, startPos, timeCount / (awakeTime / 2) - 1);
                }
            }
            else
            {
                enemyActive = false;
                timeCount = 0;
            }
        }
    }

    void MoveProcess()
    {
        if (timeCount < awakeTime)
        {
            //float speed = 1 / awakeTime / 2;
            timeCount += Time.deltaTime;

            if (timeCount < awakeTime / 2)
            {
                Vector3 scale = oriScale;
                scale.x *= -1;
                transform.localScale = scale;
                transform.position = Vector3.Lerp(startPos, endPos, timeCount / (awakeTime / 2));
            }
            else
            {
                transform.localScale = oriScale;
                transform.position = Vector3.Lerp(endPos, startPos, timeCount / (awakeTime / 2) - 1);
            }
        }
        else
        {
            timeCount = 0;
        }
    }

}
