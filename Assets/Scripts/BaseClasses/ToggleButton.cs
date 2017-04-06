using UnityEngine;
using System.Collections;

public class ToggleButton : MonoBehaviour{
	public GameObject QuestionStepGO;
	public GameObject LabelGO;
	public bool correctBool;
	string contentText{ get; set; }
	public UISprite currentSprite;
	void Start () {
		currentSprite = gameObject.GetComponent<UISprite> ();
	}

	void update() {
	}

	public void postValidation(){
		Debug.Log ("postValidation of Toggle");
		if (gameObject.GetComponent<UIToggle> ().value == correctBool) {
			correctAnimation ();
		} else {
			incorrectAnimation ();
			gameObject.GetComponent<UIToggle> ().value = correctBool;
		}
		//gameObject.SetActive(correctBool);
	}
	public void correctAnimation (){
		Debug.Log ("Correct Animation");
	}
	public void incorrectAnimation(){
		Debug.Log ("InCorrect Animation");
	}
}
