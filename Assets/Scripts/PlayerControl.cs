using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    [HideInInspector]
    public bool facingRight = false;
    public LayerMask blockingLayer;
    //public LayerMask triggerLayer;

    public float scaleX = 0.3f;
    public float scaleY = 0.1f;

    public float maxScale = 0.4f;
    public float maxY = 150;
    public float maxX = 450;

    private BoxCollider2D boxCollider;
    private Animator anim;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
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
            else
                anim.SetBool("Front", true);
            anim.SetFloat("Speed", 0);
            anim.SetFloat("SpeedV", absV);

            horizontal = 0;
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
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(xDir * scaleX, yDir * scaleY);

        Vector3 startRay = start + new Vector3(boxCollider.offset.x, boxCollider.offset.y);
        Vector3 endRay = startRay + new Vector3(xDir * scaleX, yDir * scaleY);
        
        // check bound
        if (end.x > maxX)
            end = new Vector3(maxX, end.y, end.z);
        else if (end.x < -maxX)
            end = new Vector3(-maxX, end.y, end.z);

        if (end.y > maxY)
            end = new Vector3(end.x, maxY, end.z);
        else if (end.y < -maxY)
            end = new Vector3(end.x, -maxY, end.z);

        // actual move
        if (start != end)
        {
            //boxCollider.enabled = false;

            //Cast a line from start point to end point checking collision on blockingLayer.
            RaycastHit2D hit = Physics2D.Linecast(startRay, endRay, blockingLayer);

            //Re-enable boxCollider after linecast
            //boxCollider.enabled = true;

            if (hit.transform == null)
            {
                //transform.localPosition = Vector3.Lerp(curPos, mTargetPosition, Time.deltaTime);
                transform.position = end;

                // check scale
                float index = (maxScale - 1) / maxY;
                float actualScale = index * transform.position.y + 1;

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
}
