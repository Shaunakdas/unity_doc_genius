using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class PrimeFactorController {
	public int input{ get; set; }
	public List<List<int>> primeFactorProcessList{ get; set; }
	public int primeFactorProcessCount{get; set;}
	//For Table Column count
	public int tableColumnCount{get; set;}
	public PrimeFactorController(int currInput){
		input = currInput;
		primeFactorProcessList = PrimeFactorProcessList (currInput);
	}
	public List<List<int>> PrimeFactorProcessList(int number) {
		List<List<int>> primeFactorProcessList = new List<List<int>>();
		int primeFactorProcessIndex = 0;
		for(int div = 2; div<=Mathf.Sqrt(number); div++){
			while(number%div==0){
				List<int> factorList;
				if (primeFactorProcessIndex > 0) {
					//Adding previous Step Factors 
					factorList = new List<int>(primeFactorProcessList[primeFactorProcessIndex - 1]);
					factorList.RemoveAt(factorList.Count-1);
				}else{
					factorList = new List<int>();
				}
				number = number / div;
				if (number > 1) {
					factorList.Add (div);
					factorList.Add (number);
					primeFactorProcessList.Add (factorList);
					primeFactorProcessIndex++;
				}

			}
		}
		primeFactorProcessCount = primeFactorProcessList.Count;
		return primeFactorProcessList;
	}

}
