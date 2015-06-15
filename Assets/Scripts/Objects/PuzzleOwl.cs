using UnityEngine;
using System.Collections;

public class PuzzleOwl : PuzzleObject
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activited) return;

            PlayerControl player = other.GetComponent<PlayerControl>();

            if (!player.HasPiano()) {
                Debug.Log("no piano");
                return; }

            audioSource.Play();

            //disable click open automatically
            activited = true;
            //LevelControl.instance.OnPuzzleObjectClick(this);

            tweenScale.PlayForward();


        }
    }

}
