using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class LCMController:HCFController {
	//For Table Column count
	public int tableColumnCount{get; set;}
	public LCMController(List<int> currInputList){
		base.inputList = currInputList;
		base.inputFactorList = base.getInputFactorList (currInputList);
	}

	public int getPrimeMaximumCount(List<List<int>> inputFactorList, int prime){
		int primeMaximumCount;
		List<int> primeCountList = new List<int>();
		for (int i = 0; i < inputFactorList.Count; i++) {
			primeCountList.Add(0);
			for (int j = 0; j < inputFactorList [i].Count; j++) {
				if (inputFactorList [i] [j] == prime)
					primeCountList[i]++;
			}
		}

		return primeCountList.Max ();
	}
}
