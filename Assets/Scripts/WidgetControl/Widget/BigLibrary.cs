using UnityEngine;
using System.Collections;

public class BigLibrary : MonoBehaviour {

	public int stayOnScreen = 60;

	public QuestionControl control;

	float count = 0;

	void onEnable() {
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (count < stayOnScreen) {
			count += Time.deltaTime;
		} else {
			count = 0;
			if (control != null) {
				control.AQuestionSubmit();
			}
			gameObject.SetActive(false);
		}
	}
}
