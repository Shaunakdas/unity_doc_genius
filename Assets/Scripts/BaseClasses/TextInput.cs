using UnityEngine;
using System.Collections;

public class TextInput : MonoBehaviour{
	public GameObject QuestionStepGO;
	public GameObject LabelGO;
	public int correctInt{ get; set; }
	public UISprite currentSprite;
	void Start () {
		currentSprite = gameObject.GetComponent<UISprite> ();
	}

	void update() {
	}
	public void postValidation(){
		if (LabelGO.GetComponent<UILabel> ().text == correctInt.ToString ()) {
			correctAnimation ();
		} else {
			incorrectAnimation ();
			LabelGO.GetComponent<UILabel> ().text = correctInt.ToString ();
		}
		gameObject.GetComponent<UISprite> ().enabled = false;
		gameObject.GetComponent<TweenAlpha> ().enabled = false;
	}
	public void correctAnimation (){
	}
	public void incorrectAnimation(){
	}
}
