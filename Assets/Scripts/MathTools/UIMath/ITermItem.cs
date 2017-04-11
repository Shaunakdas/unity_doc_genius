using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace UIMath{
	public interface ITermItem{
		long Value();
		long Value(long value);
		string GetVariable ();
		string ToLatexString();
	}
}
