using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace UIMath{
	public struct Term {
		List<ITermItem> _termItemList;
		public List<ITermItem> TermItemList
		{
			get { return _termItemList; }
			private set { _termItemList = value; }
		}
		public Term(long value)
		{
			_termItemList = new List<ITermItem>();
			_termItemList.Add(new TermCoefficient(value));
		}
		public Term(TermCoefficient termCoefficient)
		{
			_termItemList = new List<ITermItem>();
			_termItemList.Add(termCoefficient);
		}
		public Term(TermVariable termVariable)
		{
			_termItemList = new List<ITermItem>();
			_termItemList.Add(termVariable);
		}
//		public Term(List<TermCoefficient> termCoefficientList)
//		{
//			_termItemList = new List<ITermItem>(termCoefficientList);
//		}
//		public Term(List<TermVariable> termVariableList)
//		{
//			_termItemList = new List<ITermItem>(termVariableList);
//		}
		public bool Equals(Term other)
		{
			if (other == null)
				return false;
			bool output = false;
			if (TermItemList.Count == other.TermItemList.Count) {
				foreach (ITermItem iTermItem in TermItemList) {
					int index = TermItemList.IndexOf (iTermItem);
					if (iTermItem.Equals (other.TermItemList [index]))
						output = true;
					else
						return false;
				}
			}
			return output;
		}
		public bool Equals(object obj)
		{
			if (obj == null || !(obj is Term))
				return false;

			return Equals((Term)obj);
		}
		public long CoefficientValue()
		{
			long coefficientValue = 1;
			foreach (ITermItem iTermItem in TermItemList) {
				if(iTermItem.GetType() == typeof(UIMath.TermCoefficient))
					coefficientValue *= (long)(iTermItem.Value ());
			}
			return coefficientValue;
		}
		public long ValueAtVariableValue(List<long> valueList)
		{
			long variableValue = 1;
			int indexer = 0;
			foreach (ITermItem iTermItem in TermItemList) {
				int index = TermItemList.IndexOf (iTermItem);
				if (iTermItem.GetType () == typeof(UIMath.TermVariable)) {
					variableValue *= iTermItem.Value (valueList [indexer]);
					indexer++;
				} else {
					variableValue *= iTermItem.Value ();
				}
			}
			return variableValue;
		}
		public string ToLatexString()
		{
			string latexString = "";
			foreach (ITermItem iTermItem in TermItemList) {
				latexString += iTermItem.ToLatexString ();
			}

			return latexString;
		}

		private static Term Multiply(Term term1, Term term2)
		{
			try
			{
				checked
				{
					foreach (ITermItem iTermItem in term2.TermItemList) {
						term1.TermItemList.Add(iTermItem);
					}

					return term1;
				}
			}
			catch (OverflowException e)
			{
				throw new OverflowException("Overflow occurred while performing arithemetic operation on Term.", e);
			}
			catch (Exception e)
			{
				throw new Exception("An error occurred while performing arithemetic operation on Term.", e);
			}
		}

//		public static Term operator +(Term termCoeff1, Term termCoeff2) { return (Add(termCoeff1, termCoeff2)); }
		public static Term operator *(Term termCoeff1, Term termCoeff2) { return (Multiply(termCoeff1, termCoeff2)); }


		public static bool operator ==(Term termCoeff1, Term termCoeff2) { return termCoeff1.Equals(termCoeff2); }
		public static bool operator !=(Term termCoeff1, Term termCoeff2) { return (!termCoeff1.Equals(termCoeff2)); }

	}

}
