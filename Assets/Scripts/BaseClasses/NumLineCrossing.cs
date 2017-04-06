using UnityEngine;
using System.Collections;

public class NumLineCrossing : MonoBehaviour {
	public GameObject DropHolderGO;
	public GameObject TextInputPF;
	public GameObject LabelGO;
	public GameObject ToggleGO;
	public UISprite currentSprite;
	public GameObject ContainerGO;

	public GameObject QuestionStepGO;
	public int correctInt{ get; set; }
	public bool actionRequired{ get; set; }
	void Start () {
		currentSprite = ContainerGO.GetComponent<UISprite> ();
	}
	public void updateLabel(string text){
		Debug.Log ("UpdateText of NumLineCrossing entered");
		if (LabelGO != null) {
			Debug.Log ("UpdateText of NumLineCrossing entered");
			LabelGO.GetComponent<UILabel> ().text = text;
		} else {
			Debug.Log ("UpdateText of NumLineCrossing is not working");
		}
	}
	void update() {
	}
	public void postValidation(){
		if (DropHolderGO.transform.childCount > 0) {
			//An object was dropped here.
			if (DropHolderGO.transform.GetChild (0).gameObject.GetComponent<DragSource> ().LabelGO.GetComponent<UILabel> ().text == correctInt.ToString ()) {
				//Correct Object was dropped
				correctAnimation ();
			} else {
				//Wrong object was dropped
				incorrectAnimation ();
			}
		} else if (actionRequired) {
			//An object was needed to be dropped here.
			GameObject optionGO = instantiateNGUIGameObject (TextInputPF, DropHolderGO.transform);
			optionGO.GetComponent<TextInput> ().LabelGO.GetComponent<UILabel> ().text = correctInt.ToString ();
			incorrectAnimation ();
		} else {
			Debug.Log ("Nothing is needed");
		}	
	}
	GameObject instantiateNGUIGameObject(GameObject prefabHolder,Transform parentTransform){
		GameObject currentGO = (GameObject)Instantiate(prefabHolder,parentTransform);
		currentGO.transform.localScale = new Vector3 (1f, 1f, 1f);
		return currentGO;
	}
	public void correctAnimation (){
		Debug.Log ("Correct Animation");
	}
	public void incorrectAnimation(){
		Debug.Log ("InCorrect Animation");
	}
}
