using UnityEngine;
using System.Collections;

public class PuzzleOwl : PuzzleObject
{
    public GameObject boredText;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activited) return;

            PlayerControl player = other.GetComponent<PlayerControl>();

            if (!player.HasPiano())
            {
                boredText.gameObject.SetActive(true);
                Debug.Log("no piano");
                return;
            }

            if (GameManager.instance != null)
            {
                if (GameManager.instance.allowMusic)
                {
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.Play();
            }

            //disable click open automatically
            activited = true;
            //LevelControl.instance.OnPuzzleObjectClick(this);

            tweenScale.PlayForward();
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);

        if (other.tag == "Player")
        {
            boredText.gameObject.SetActive(false);
        }
    }

}
