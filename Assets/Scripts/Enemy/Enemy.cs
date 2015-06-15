using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public bool timeSensitive;
    public int sleepTime;
    public int awakeTime;

    public float speed;

    public bool roundWay;

    public Vector3 startPos;
    public Vector3 endPos;

    public Transform[] routerPositions;

    private int currentPosIndex;

    public bool enemyActive;

    private float timeCount, sleepTimeCount;

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
            if (enemyActive)
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

    //void MoveProcess()
    //{
    //    if (timeCount < awakeTime)
    //    {
    //        //float speed = 1 / awakeTime / 2;
    //        timeCount += Time.deltaTime;

    //        if (timeCount < awakeTime / 2)
    //        {
    //            Vector3 scale = oriScale;
    //            scale.x *= -1;
    //            transform.localScale = scale;
    //            transform.position = Vector3.Lerp(startPos, endPos, timeCount / (awakeTime / 2));
    //        }
    //        else
    //        {
    //            transform.localScale = oriScale;
    //            transform.position = Vector3.Lerp(endPos, startPos, timeCount / (awakeTime / 2) - 1);
    //        }
    //    }
    //    else
    //    {
    //        timeCount = 0;
    //    }
    //}

    bool isReturning;
    void MoveProcess()
    {
        if (roundWay)
        {
            if (sleepTimeCount < sleepTime)
            {
                sleepTimeCount += Time.deltaTime;
            }
            else
            {
                Transform fromPos = routerPositions[0];
                Transform targetPos = routerPositions[currentPosIndex + 1];

                float disc = Vector3.Distance(fromPos.position, targetPos.position);
                float corvedred = timeCount * speed;

                if (isReturning)
                {
                    if (fromPos.position.x > targetPos.position.x)
                    {
                        Vector3 scale = oriScale;
                        scale.x *= -1;
                        transform.localScale = scale;
                    }
                    else
                    {
                        transform.localScale = oriScale;
                    }
                    transform.position = Vector3.Lerp(targetPos.position, fromPos.position, corvedred / disc);
                }
                else
                {
                    if (fromPos.position.x < targetPos.position.x)
                    {
                        Vector3 scale = oriScale;
                        scale.x *= -1;
                        transform.localScale = scale;
                    }
                    else
                    {
                        transform.localScale = oriScale;
                    }
                    transform.position = Vector3.Lerp(fromPos.position, targetPos.position, corvedred / disc);
                }

                

                if (!isReturning && transform.position == targetPos.position)
                {
                    isReturning = true;
                    timeCount = 0;
                }
                else if (isReturning && transform.position == fromPos.position)
                {
                    isReturning = false;
                    sleepTimeCount = 0;
                    timeCount = 0;
                    currentPosIndex = (currentPosIndex + 1) % routerPositions.Length;
                    if (currentPosIndex == routerPositions.Length - 1)
                        currentPosIndex = 0;
                }
                else
                {
                    timeCount += Time.deltaTime;
                }
            }
        }
        else
        {
            Transform fromPos = routerPositions[currentPosIndex];
            Transform targetPos = routerPositions[(currentPosIndex + 1) % routerPositions.Length];

            float disc = Vector3.Distance(fromPos.position, targetPos.position);
            float corvedred = timeCount * speed;

            transform.position = Vector3.Lerp(fromPos.position, targetPos.position, corvedred / disc);

            if (fromPos.position.x < targetPos.position.x)
            {
                Vector3 scale = oriScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
            else
            {
                transform.localScale = oriScale;
            }

            if (transform.position == targetPos.position)
            {
                timeCount = 0;
                currentPosIndex = (currentPosIndex + 1) % routerPositions.Length;
            }
            else
            {
                timeCount += Time.deltaTime;
            }
        }


    }

}
