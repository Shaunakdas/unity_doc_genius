using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace UIMath{
	public interface IExpressionItem{
		long Value();
		long Value(List<long> valueList);
		string ToLatexString();
		List<string> GetVariableList ();
	}
}
