using UnityEngine;
using System.Collections;

public class DragSource : MonoBehaviour{
	public GameObject QuestionStepGO;
	int correctInt{ get; set; }
	public UISprite currentSprite;
	public GameObject LabelGO;
	void Start () {
		currentSprite = gameObject.GetComponent<UISprite> ();
	}

	void update() {
	}
}
