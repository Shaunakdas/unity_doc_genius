using UnityEngine;
using System.Collections;
using System;
namespace UIMath{
	public struct TermVariable : ITermItem {
		string _variable;
		public string Variable
		{
			get { return _variable; }
			private set { _variable = value; }
		}
		long _exponent;
		public long Exponent
		{
			get { return _exponent; }
			private set { _exponent = value; }
		}
		public TermVariable(string variableString)
		{
			_variable = variableString;
			_exponent = 1;
		}
		public TermVariable(string variableString, long exponent)
		{
			_variable  = variableString;
			_exponent = exponent;
		}
		public bool Equals(TermVariable other)
		{
			if (other == null)
				return false;

			return (Variable == other.Variable && Exponent == other.Exponent);
		}
		public bool Equals(object obj)
		{
			if (obj == null || !(obj is TermVariable))
				return false;

			return Equals((TermVariable)obj);
		}
		public long Value(long value)
		{
			return (long)Math.Pow (value, this.Exponent);
		}
		public long Value()
		{
			return (long)1;
		}
		public string GetVariable(){
			return this.Variable;
		}
		public string ToLatexString()
		{

			string sb = "";
			sb+= this.Variable.ToString();
			sb+= "^{";
			sb+= this.Exponent.ToString();
			sb+= "}";

			return sb;
		}
			
		public static bool operator ==(TermVariable termCoeff1, TermVariable termCoeff2) { return termCoeff1.Equals(termCoeff2); }
		public static bool operator !=(TermVariable termCoeff1, TermVariable termCoeff2) { return (!termCoeff1.Equals(termCoeff2)); }


	}

}
