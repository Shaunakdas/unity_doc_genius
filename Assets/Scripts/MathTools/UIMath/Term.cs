using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace UIMath{
	public struct Term {
		List<TermCoefficient> _termCoefficientList;
		public List<TermCoefficient> TermCoefficientList
		{
			get { return _termCoefficientList; }
			private set { _termCoefficientList = value; }
		}
		List<TermVariable> _termVariableList;
		public List<TermVariable> TermVariableList
		{
			get { return _termVariableList; }
			private set { _termVariableList = value; }
		}
		public Term(long value)
		{
			_termCoefficientList = new List<TermCoefficient>();
			_termCoefficientList.Add(new TermCoefficient(value));
		}
		public Term(TermCoefficient termCoefficient)
		{
			_termCoefficientList = new List<TermCoefficient>();
			_termCoefficientList.Add(termCoefficient);
			_termVariableList = new List<TermVariable>();
		}
		public Term(TermVariable termVariable)
		{
			_termVariableList = new List<TermVariable>();
			_termVariableList.Add(termVariable);
			_termCoefficientList = new List<TermCoefficient>();
		}
		public Term(List<TermCoefficient> termCoefficientList)
		{
			_termCoefficientList = new List<TermCoefficient>(termCoefficientList);
			_termVariableList = new List<TermVariable>();
		}
		public Term(List<TermVariable> termVariableList)
		{
			_termVariableList = new List<TermVariable>(termVariableList);
			_termCoefficientList = new List<TermCoefficient>();
		}
		public bool Equals(Term other)
		{
			if (other == null)
				return false;
			bool output = false;
			if (TermCoefficientList.Count == other.TermCoefficientList.Count) {
				foreach (TermCoefficient termCoefficient in TermCoefficientList) {
					int index = TermCoefficientList.IndexOf (termCoefficient);
					if (TermCoefficient.Equals (other.TermCoefficientList [index]))
						output = true;
					else
						return false;
				}
			}
			if (TermVariableList.Count == other.TermVariableList.Count) {
				foreach (TermVariable termVariable in TermVariableList) {
					int index = TermVariableList.IndexOf (termVariable);
					if (TermVariable.Equals (other.TermVariableList [index]))
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
			foreach (TermCoefficient termCoefficient in TermCoefficientList) {
				coefficientValue *= termCoefficient.Value ();
			}
			return coefficientValue;
		}
		public long ValueAtVariableValue(List<long> valueList)
		{
			int variableValue = 1;
			foreach (TermVariable termVariable in TermCoefficientList) {
				int index = TermCoefficientList.IndexOf (termVariable);
				variableValue *= termVariable.Value (valueList[index]);
			}
			return variableValue;
		}
		public string ToLatexString()
		{
			string latexString = "";
			int coefficientValue = 1;
			foreach (TermCoefficient termCoefficient in TermCoefficientList) {
				latexString += termCoefficient.ToLatexString ();
			}
			foreach (TermVariable termVariable in TermVariableList) {
				latexString += termVariable.ToLatexString ();
			}
			return sb;
		}

		private static Term Multiply(Term term1, Term term2)
		{
			try
			{
				checked
				{
					foreach (TermCoefficient termCoefficient in term2.TermCoefficientList) {
						term1.TermCoefficientList.Add(termCoefficient);
					}
					foreach (TermVariable termVariable in term2.TermVariableList) {
						term1.TermVariableList.Add(termVariable);
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
