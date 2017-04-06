using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Comparision : MonoBehaviour {
	public List<ComparisionOption> optionTextList;
	public GameObject OptionTableGO;
	public GameObject ComparisionOptionPrefab;
	public GameObject TrialContainer;

	public void addToOptionList(string opText,int correctOpOrder,string opCorrectionText){
		ComparisionOption option = new ComparisionOption (opText,correctOpOrder,opCorrectionText);
		Debug.Log (option.text);
		Debug.Log (optionTextList.Count);
		optionTextList.Add (option);

	}

	public GameObject addComparisionOption(GameObject parent,bool sorted){
		ComparisionOption option = getRandomOption ();
		GameObject optionGO = instantiateNGUIGameObject (ComparisionOptionPrefab, parent.transform);
		optionGO.GetComponent<ComparisionOptionButton> ().ReparentTarget = OptionTableGO;
		optionGO.GetComponent<ComparisionOptionButton> ().ComparisionHandler = gameObject;
		getChildGameObject (optionGO, "Label").GetComponent<UILabel> ().text = option.text;

		optionGO.transform.localPosition = new Vector3 (0F, 0f, 0f);
		optionGO.name = option.correctOrder.ToString () + optionGO.name;
		Debug.Log (optionGO.transform.position.ToString ());
		if (sorted){
			optionTextList.Remove (option);

		}
		return optionGO;
	}
	public ComparisionOption getRandomOption(){
		System.Random rnd = new System.Random();
		return optionTextList[rnd.Next(optionTextList.Count)];

	}
	// Use this for initialization
	void Start () {
		optionTextList = new List<ComparisionOption> ();
		addToOptionList ("25cm", 0, "haha");
		addToOptionList ("50cm", 1, "haha");
//		addToOptionList ("100cm", 2, "haha");
		addComparisionOption (OptionTableGO,true);

		GameObject optionGO = addComparisionOption (TrialContainer,false);


	}
	public void notifyDragDropEnd(GameObject optionGO){
		Debug.Log ("Comparision DragDrop finished");
		checkForOptionOrder (optionGO);
	}
	public void checkForOptionOrder(GameObject optionGO){
		if(optionGO.transform.parent.gameObject == OptionTableGO){
			string optionText = getChildGameObject (optionGO, "Label").GetComponent<UILabel> ().text;
			ComparisionOption option = optionTextList[0];
			foreach(ComparisionOption item in optionTextList){
				if(item.text == optionText)
					option= item;
			}

//			if(optionGO.transform.GetSiblingIndex() != option.correctOrder){
//				Debug.Log("IncorrectOrder"+optionGO.transform.GetSiblingIndex()+option.correctOrder);
//
////				optionGO.transform.SetSiblingIndex (option.correctOrder);
////				OptionTableGO.GetComponent<UIGrid> ().Reposition ();
////				Debug.Log("Order corrected"+optionGO.transform.GetSiblingIndex()+option.correctOrder);
//			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
	GameObject instantiateNGUIGameObject(GameObject prefabHolder,Transform parentTransform){
		GameObject currentGO = (GameObject)Instantiate(prefabHolder,parentTransform);
		currentGO.transform.localScale = new Vector3 (1f, 1f, 1f);
		return currentGO;
	}
	GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		//Author: Isaac Dart, June-13.
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}
}
public class ComparisionOption{
	public string text{ get; set; }
	public int correctOrder{ get; set; }
	public string correctionText{ get; set; }
	public ComparisionOption (string opText,int correctOpOrder,string opCorrectionText){
		text = opText;
		correctOrder = correctOpOrder;
		correctionText = opCorrectionText;
	}
	public bool sorted{ get; set; }
}