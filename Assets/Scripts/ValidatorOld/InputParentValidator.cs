using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputParentValidator : MonoBehaviour {
	public enum InputType{InputSingleCorrect,InputMultipleCorrect,Toggle};
	public List<string> correctValueList;
	public List<bool> correctToggleList;
	public InputType childButtonType;
	// Use this for initialization
	void Start () {
		childButtonType = InputType.InputSingleCorrect;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
