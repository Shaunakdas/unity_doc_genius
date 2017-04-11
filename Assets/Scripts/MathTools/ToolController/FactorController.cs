using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class FactorController {
	public int input{ get; set; }

	//For Table Column count
	public int tableColumnCount{get; set;}
	public FactorController(int currInput){
		input = currInput;
	}
	public List<int> Factor(int number) {
		List<int> factors = new List<int>();
		int max = (int)Mathf.Sqrt(number);  //round down
		for(int factor = 1; factor <= max; ++factor) { //test from 1 to the square root, or the int below it, inclusive.
			if(number % factor == 0) {
				factors.Add(factor);
				if(factor != number/factor) { // Don't add the square root twice!  Thanks Jon
					factors.Add(number/factor);
				}
			}
		}
		return factors;
	}
	public int FactorStepCount(int number) {
		int factorStepCount=0;
		int max = (int)Mathf.Sqrt(number);  //round down
		for(int factor = 1; factor <= max; ++factor) { //test from 1 to the square root, or the int below it, inclusive.
			if(number % factor == 0) {
				factorStepCount++;
			}
		}
		return factorStepCount;
	}

}
