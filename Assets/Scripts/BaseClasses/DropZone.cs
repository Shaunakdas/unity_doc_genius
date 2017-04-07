using UnityEngine;
using System.Collections;

public class DropZone : MonoBehaviour{
	public GameObject TextInputPF;
	public GameObject QuestionStepGO;
	public GameObject DropHolderGO;
	public GameObject ContainerGO;
	public string correctText{ get; set; }
	public UISprite currentSprite;
	void Start () {
		currentSprite = ContainerGO.GetComponent<UISprite> ();
	}
	public bool actionRequired{ get; set;}
	void update() {
	}
	public void postValidation(){
		if (DropHolderGO.transform.childCount > 0) {
			//An object was dropped here.
			if (DropHolderGO.transform.GetChild (0).gameObject.GetComponent<DragSource> ().LabelGO.GetComponent<UILabel> ().text == correctText) {
				//Correct Object was dropped
				correctAnimation ();
			} else {
				//Wrong object was dropped
				incorrectAnimation ();  
			}
		} else if (actionRequired) {
			//An object was needed to be dropped here.
			GameObject optionGO = NGUITools.AddChild(DropHolderGO,TextInputPF);
			optionGO.GetComponent<TextInput> ().LabelGO.GetComponent<UILabel> ().text = correctText;
			incorrectAnimation ();
		} else {
			Debug.Log ("Nothing is needed");
		}	
	}
	public void correctAnimation (){
		Debug.Log ("Correct Animation");
	}
	public void incorrectAnimation(){
		Debug.Log ("InCorrect Animation");
	}
}
