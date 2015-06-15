using UnityEngine;
using System.Collections;

public class PuzzleFlashlight : PuzzleObject
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activited) return;

            PlayerControl player = other.GetComponent<PlayerControl>();
            player.SetFlashlightActive(true);
            
            audioSource.Play();

            //disable click open automatically
            activited = true;
            //LevelControl.instance.OnPuzzleObjectClick(this);

            tweenScale.PlayForward();


        }
    }
}
