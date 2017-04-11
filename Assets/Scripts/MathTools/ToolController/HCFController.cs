using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class HCFController {
	public List<int> inputList;
	public List<int> commonFactorList;
	public List<List<int>> inputFactorList;
	//For Table Column count
	public int tableColumnCount{get; set;}
	public HCFController(){
	}
	public HCFController(List<int> currInputList){
		inputList = currInputList;
		inputFactorList = getInputFactorList (currInputList);
	}
	public List<List<int>> getInputFactorList(List<int> inputList) {
		List<List<int>> inputFactorList = new List<List<int>>();
		for (int inputIndex = 0; inputIndex < inputList.Count; inputIndex++) {
			inputFactorList.Add (PrimeFactorlist(inputList [inputIndex]));
		}
		return inputFactorList;
	}
	public static List<int> PrimeFactorlist(int number){
		var primes = new List<int>();

		for(int div = 2; div<=number; div++){
			while(number%div==0){
				primes.Add(div);
				number = number / div;
			}
		}
		return primes;
	}
	public List<List<int>> getPrimeHighlightIndexList(List<List<int>> inputFactorList, int prime){
		List<List<int>> primeHighlightIndexList = new List<List<int>>();
		for (int i = 0; i < inputFactorList.Count; i++) {
			List<int> highlightRow = new List<int> ();
			for (int j = 0; j < inputFactorList [i].Count; j++) {
				if (inputFactorList [i] [j] == prime)
					highlightRow.Add (j);
			}
			primeHighlightIndexList.Add (highlightRow);
		}
		return primeHighlightIndexList;
	}
	public int getPrimeMinimumCount(List<List<int>> inputFactorList, int prime){
		int primeMinimumCount;
		List<int> primeCountList = new List<int>();
		for (int i = 0; i < inputFactorList.Count; i++) {
			primeCountList.Add(0);
			for (int j = 0; j < inputFactorList [i].Count; j++) {
				if (inputFactorList [i] [j] == prime)
					primeCountList[i]++;
			}
		}

		return primeCountList.Min ();
	}
}
