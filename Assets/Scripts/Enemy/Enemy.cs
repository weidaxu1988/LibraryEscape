using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public bool scaleDown = false;

    public bool timeSensitive;
    public int sleepTime;
    public int awakeTime;

    public float speed;

    public bool roundWay;

    public TweenScale tweenScale;

    public Vector3 startPos;
    public Vector3 endPos;

    public Transform[] routerPositions;

    private int currentPosIndex;

    public bool enemyActive;
    public bool catchable;

    private float timeCount, sleepTimeCount;

    private Vector3 oriScale;
    private Vector3 deltaScale;

    void Start()
    {
        oriScale = transform.localScale;
        deltaScale = oriScale / 6;
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

    public void ScaleDown()
    {
        if (scaleDown)
        {
            Vector3 scale = transform.localScale;

            int curSign = scale.x > 0 ? 1 : -1;
            scale = new Vector3((Mathf.Abs(scale.x) - Mathf.Abs(deltaScale.x)) * curSign, scale.y - deltaScale.y, scale.z - deltaScale.z);

            transform.localScale = scale;

            int oriSign = oriScale.x > 0 ? 1 : -1;
            oriScale = new Vector3(Mathf.Abs(scale.x) * oriSign, scale.y, scale.z);

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
            //if (catchable)
            //{
            //    GhostGetCaught();
            //}
            //else
            //{
                if (enemyActive)
                    GhostCaught();
            //}
        }
    }

    void GhostGetCaught()
    {
        LevelControl.instance.AllowAnswerQuestion();
        gameObject.SetActive(false);
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
                if (sleepTime - timeCount < 2)
                {
                    tweenScale.enabled = true;
                }

                timeCount += Time.deltaTime;
            }
            else
            {
                tweenScale.enabled = false;
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


                    transform.localScale = oriScale;
                    transform.position = Vector3.Lerp(startPos, endPos, timeCount / (awakeTime / 2));
                }
                else
                {
                    Vector3 scale = oriScale;
                    scale.x *= -1;
                    transform.localScale = scale;

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
                        transform.localScale = oriScale;
                    }
                    else
                    {
                        Vector3 scale = oriScale;
                        scale.x *= -1;
                        transform.localScale = scale;

                    }
                    transform.position = Vector3.Lerp(targetPos.position, fromPos.position, corvedred / disc);
                }
                else
                {
                    if (fromPos.position.x < targetPos.position.x)
                    {
                        transform.localScale = oriScale;
                    }
                    else
                    {
                        Vector3 scale = oriScale;
                        scale.x *= -1;
                        transform.localScale = scale;

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
                transform.localScale = oriScale;
            }
            else
            {
                Vector3 scale = oriScale;
                scale.x *= -1;
                transform.localScale = scale;
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
