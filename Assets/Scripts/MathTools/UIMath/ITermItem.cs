using UnityEngine;
using System.Collections;
namespace UIMath{
	public interface ITermItem{
		long Value();
		long Value(long value);
		string ToLatexString();
	}
}
