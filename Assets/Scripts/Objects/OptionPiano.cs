using UnityEngine;
using System.Collections;

public class OptionPiano : MonoBehaviour {

	public LevelControl levelControl;

    public GameObject mText;

    private TweenScale tweenScale;
    //private CircleCollider2D circleCollider;

    private AudioSource audioSource;

    void Awake()
    {
        //circleCollider = GetComponent<CircleCollider2D>();
        tweenScale = GetComponentInChildren<TweenScale>();
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerControl player = other.GetComponent<PlayerControl>();
            player.SetPianoActive(true);

            mText.SetActive(false);

            if (GameManager.instance != null)
            {
                if (GameManager.instance.allowMusic)
                {
                    audioSource.Play();
					if (levelControl != null) {
						levelControl.muteBackgroundMusic(true);
					}

					StartCoroutine(stopMusic());

                }
            }
            else
            {
                audioSource.Play();
            }

            tweenScale.PlayForward();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            tweenScale.PlayReverse();
        }
    }

	IEnumerator stopMusic() {
		yield return new WaitForSeconds(48f);
		audioSource.Stop ();

		if (GameManager.instance.allowMusic)
		{
			if (levelControl != null) {
				levelControl.muteBackgroundMusic(false);
			}
		}
	}
}
