using UnityEngine;
using System.Collections;

public class DropZone : MonoBehaviour{
	public GameObject QuestionStepGO;
	int correctInt{ get; set; }
	public UISprite currentSprite;
	void Start () {
		currentSprite = gameObject.GetComponent<UISprite> ();
	}

	void update() {
	}
}
