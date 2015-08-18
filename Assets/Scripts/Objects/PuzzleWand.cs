using UnityEngine;
using System.Collections;

public class PuzzleWand : PuzzleObject {

	public GameObject uiWand;
	public GameObject normalWand;

	protected override void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			if (activited) return;
			
			//disable click open automatically
			activited = true;
			//LevelControl.instance.OnPuzzleObjectClick(this);
			
			tweenScale.PlayForward();
		}
	}
	
	protected override void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") {
			if (activited) {
				
				if (!uiWand.activeSelf) {
					uiWand.SetActive (true);
				}
				
				if (normalWand.activeSelf) {
					normalWand.SetActive (false);
				}
			}
		}

		base.OnTriggerExit2D(other);


	}
}
