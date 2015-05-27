using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //LevelControl.instance.StartQuestion();
        }
    }

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        if (!activited) return;

    //        activited = false;
    //        tweenScale.PlayReverse();
    //    }
    //}
}
