using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    [HideInInspector]
    public bool facingRight = false;
    public LayerMask blockingLayer;
    //public LayerMask triggerLayer;

    public int delay = 60;

    public float scaleX = 0.3f;
    public float scaleY = 0.1f;

    public float[] maxScales;
    public float[] maxYs;
    public float maxX = 450;

    public Enemy[] timeEnemies;

    private BoxCollider2D boxCollider;
    private Animator anim;

    public bool flashlightActive, pianoActive, fireKillerActive, cheeseActive;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        ResetTimeCount();
    }

    void Update()
    {


        if (LevelControl.instance != null && LevelControl.instance.isGamePaused)
        {
            anim.SetFloat("Speed", 0);
            anim.SetFloat("SpeedV", 0);
            return;
        }

        float horizontal = 0;
        float vertical = 0;

#if UNITY_STANDALONE || UNITY_WEBPLAYER

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        
#endif


        float absH = Mathf.Abs(horizontal);
        float absV = Mathf.Abs(vertical);



        if (absH > absV)
        {
            anim.SetFloat("Speed", absH);
            anim.SetFloat("SpeedV", 0);
            vertical = 0;
        }
        else if (absH < absV)
        {
            if (vertical > 0)
                anim.SetBool("Front", false);
            else if (vertical < 0)
                anim.SetBool("Front", true);

            anim.SetFloat("Speed", 0);
            anim.SetFloat("SpeedV", absV);

            horizontal = 0;
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }


        if (horizontal > 0 && !facingRight)
            Flip();

        else if (horizontal < 0 && facingRight)
            Flip();

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove(horizontal, vertical);
            //AttemptMove<PuzzlObject>((int)horizontal, (int)vertical);
        }
    }

    void AttemptMove(float xDir, float yDir)
    {


        CheckTimeSensitiveEnemies();

        float actualScale = (maxScales[1] - maxScales[0]) * (transform.position.y - maxYs[1]) / (maxYs[0] - maxYs[1]) + maxScales[0];

        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(xDir * scaleX, yDir * scaleY);

        //Vector3 startRay = start + new Vector3(boxCollider.offset.x, boxCollider.offset.y);
        Vector3 startRay = start;
        Vector3 endRay = startRay + new Vector3(xDir * scaleX, yDir * scaleY);

        // check bound
        if (end.x > maxX)
            end = new Vector3(maxX, end.y, end.z);
        else if (end.x < -maxX)
            end = new Vector3(-maxX, end.y, end.z);

        if (end.y > maxYs[0])
            end = new Vector3(end.x, maxYs[0], end.z);
        else if (end.y < maxYs[1])
            end = new Vector3(end.x, maxYs[1], end.z);

        // actual move
        if (start != end)
        {
            //boxCollider.enabled = false;

            //Cast a line from start point to end point checking collision on blockingLayer.
            RaycastHit2D hit = Physics2D.Linecast(startRay, endRay, blockingLayer);

Debug.DrawLine(startRay, startRay+ new Vector3(xDir, yDir), Color.red);
            //Re-enable boxCollider after linecast
            //boxCollider.enabled = true;

            if (hit.transform == null)
            {
                //transform.localPosition = Vector3.Lerp(curPos, mTargetPosition, Time.deltaTime);
                transform.position = end;

                // check scale
                //float index = (maxScale - 1) / (maxYs[0] - maxYs[1]);
                //float actualScale = index * transform.position.y + 1;
                //float actualScale = index * transform.position.y + 1;
                actualScale = (maxScales[1] - maxScales[0]) * (transform.position.y - maxYs[1]) / (maxYs[0] - maxYs[1]) + maxScales[0];

                Vector3 curScale = transform.localScale;
                transform.localScale = new Vector3(curScale.x < 0 ? -actualScale : actualScale, actualScale, 1);
            }
        }
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void ResetTimeCount()
    {
        anim.SetBool("Worrit", false);
        CancelInvoke("StartWorrit");
        Invoke("StartWorrit", delay);
    }

    void StartWorrit()
    {
        anim.SetBool("Worrit", true);
    }

    void CheckTimeSensitiveEnemies()
    {
        foreach (Enemy e in timeEnemies)
        {
            if (e.enemyActive && e.timeSensitive)
            {
                LevelControl.instance.GameFailed();
            }
        }
    }

    public void SetFireKillerActive(bool active)
    {
        fireKillerActive = active;
    }

    public bool HasFireKiller()
    {
        return fireKillerActive;
    }

    public void SetPianoActive(bool active)
    {
        pianoActive = active;
    }

    public bool HasPiano()
    {
        return pianoActive;
    }

    public void SetCheeseActive(bool active)
    {
        cheeseActive = active;
    }

    public bool HasCheese()
    {
        return cheeseActive;
    }

    public void SetFlashlightActive(bool active)
    {
        flashlightActive = active;
    }

    public bool HasFlashlight()
    {
        return flashlightActive;
    }
}
