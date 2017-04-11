using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
namespace UIMath{
	public struct Expression :IExpressionItem {
		List<IExpressionItem> _expressionItemList;
		public List<IExpressionItem> ExpressionItemList
		{
			get { return _expressionItemList; }
			private set { _expressionItemList = value; }
		}
		public Expression()
		{
			_expressionItemList = new List<IExpressionItem>();
		}
		public Expression(long value)
		{
			_expressionItemList = new List<IExpressionItem>();
			_expressionItemList.Add(new Term(value));
		}
		public Expression(Term term)
		{
			_expressionItemList = new List<IExpressionItem>();
			_expressionItemList.Add(term);
		}
		public Expression(MathOperator mathOperator)
		{
			_expressionItemList = new List<IExpressionItem>();
			_expressionItemList.Add(mathOperator);
		}
		public Expression(List<IExpressionItem> expressionItemList)
		{
			_expressionItemList = new List<IExpressionItem>(expressionItemList);
		}
		public void AddTerm(Term term)
		{
			_expressionItemList.Add(term);
		}
		public void AddExpressionVariable(MathOperator mathOperator)
		{
			_expressionItemList.Add(mathOperator);
		}
		public bool Equals(Expression other)
		{
			if (other == null)
				return false;
			bool output = false;
			if (ExpressionItemList.Count == other.ExpressionItemList.Count) {
				foreach (IExpressionItem iExpressionItem in ExpressionItemList) {
					int index = ExpressionItemList.IndexOf (iExpressionItem);
					if (iExpressionItem.Equals (other.ExpressionItemList [index]))
						output = true;
					else
						return false;
				}
			}
			return output;
		}
		public bool Equals(object obj)
		{
			if (obj == null || !(obj is Expression))
				return false;

			return Equals((Expression)obj);
		}
		//Work Pending
		public long Value(List<long> valueList)
		{
			List<string> completeVariableList = new List<string>(GetVariableList());
			long variableValue=0;
			int indexer = 0;
			foreach (IExpressionItem iExpressionItem in ExpressionItemList) {
				
				if (iExpressionItem.GetType () == typeof(UIMath.Term)) {
					List<long> termValueList = new List<long>();
					List<string> termVariableList = iExpressionItem.GetVariableList ();

					foreach (string variable in termVariableList) {
						int termVariableIndex = termVariableList.IndexOf (variable);
						int variableIndex = completeVariableList.IndexOf (variable);
						variableValue += 0;
					}
				} else {
					variableValue += iExpressionItem.Value ();
				}
			}
			return variableValue;
		}
		public long Value()
		{
			long variableValue = 1;
			int indexer = 0;
			foreach (IExpressionItem iExpressionItem in ExpressionItemList) {
				int index = ExpressionItemList.IndexOf (iExpressionItem);
				if (iExpressionItem.GetType () == typeof(UIMath.Term)) {
					variableValue *= iExpressionItem.Value ();
					indexer++;
				} else {
					variableValue *= iExpressionItem.Value ();
				}
			}
			return variableValue;
		}
		public int VariableCount()
		{
			int indexer = 0;
			foreach (IExpressionItem iExpressionItem in ExpressionItemList) {
				if (iExpressionItem.GetType () == typeof(UIMath.Term)) {
					indexer++;
				}
			}
			return indexer;
		}
		public List<string> GetVariableList()
		{
			List<string> variableList = new List<string>();
			int indexer = 0;
			foreach (IExpressionItem iExpressionItem in ExpressionItemList) {
				if (iExpressionItem.GetType () == typeof(UIMath.Term)) {
					List<string> termVariableList = new List<string>(iExpressionItem.GetVariableList()); 
					foreach (String variable in termVariableList) {
						if (variableList.IndexOf (variable) == -1) {
							variableList.Add (variable);
						}
					}
				}
			}
			return variableList;
		}
		public string ToLatexString()
		{
			string latexString = "";
			foreach (IExpressionItem iExpressionItem in ExpressionItemList) {
				latexString += iExpressionItem.ToLatexString ();
			}

			return latexString;
		}
		private static Expression Negate(Expression exp1)
		{
			int exp1_count=exp1.ExpressionItemList.Count;
			if(!(exp1.ExpressionItemList[0].Equals(new MathOperator("(")) && (exp1.ExpressionItemList[exp1_count-1].Equals(new MathOperator(")"))))){
				exp1.ExpressionItemList.Insert(exp1_count,new MathOperator(")"));
				exp1.ExpressionItemList.Insert(0,new MathOperator("("));
			}
			exp1.ExpressionItemList.Insert(0,new MathOperator("-"));
			return exp1;

		}
		private static Expression Multiply(Expression exp1, Expression exp2)
		{
			try
			{
				checked
				{	
					int exp1_count=exp1.ExpressionItemList.Count;
					int exp2_count=exp2.ExpressionItemList.Count;
					if(!(exp1.ExpressionItemList[0].Equals(new MathOperator("(")) && (exp1.ExpressionItemList[exp1_count-1].Equals(new MathOperator(")"))))){
						exp1.ExpressionItemList.Insert(exp1_count,new MathOperator(")"));
						exp1.ExpressionItemList.Insert(0,new MathOperator("("));
					}
					if(!(exp2.ExpressionItemList[0].Equals(new MathOperator("(")) && (exp2.ExpressionItemList[exp2_count-1].Equals(new MathOperator(")"))))){
						exp2.ExpressionItemList.Insert(exp2_count,new MathOperator(")"));
						exp2.ExpressionItemList.Insert(0,new MathOperator("("));
					}	
					exp1.ExpressionItemList.Concat(exp2.ExpressionItemList);
					return exp1;
				}
			}
			catch (OverflowException e)
			{
				throw new OverflowException("Overflow occurred while performing arithemetic operation on Expression.", e);
			}
			catch (Exception e)
			{
				throw new Exception("An error occurred while performing arithemetic operation on Expression.", e);
			}
		}
		private static Expression Add(Expression exp1, Expression exp2)
		{
			try
			{
				checked
				{	
					int exp2_count=exp2.ExpressionItemList.Count;
					if(!(exp2.ExpressionItemList[0].Equals(new MathOperator("(")) && (exp2.ExpressionItemList[exp2_count-1].Equals(new MathOperator(")"))))){
						exp2.ExpressionItemList.Insert(exp2_count,new MathOperator(")"));
						exp2.ExpressionItemList.Insert(0,new MathOperator("("));
					}	
					exp1.ExpressionItemList.Add(new MathOperator("+"));
					exp1.ExpressionItemList.Concat(exp2.ExpressionItemList);
					return exp1;
				}
			}
			catch (OverflowException e)
			{
				throw new OverflowException("Overflow occurred while performing arithemetic operation on Expression.", e);
			}
			catch (Exception e)
			{
				throw new Exception("An error occurred while performing arithemetic operation on Expression.", e);
			}
		}
		//		public static Expression operator +(Expression expressionCoeff1, Expression expressionCoeff2) { return (Add(expressionCoeff1, expressionCoeff2)); }
		public static Expression operator *(Expression expressionCoeff1, Expression expressionCoeff2) { return (Multiply(expressionCoeff1, expressionCoeff2)); }
		public static Expression operator +(Expression expressionCoeff1, Expression expressionCoeff2) { return (Add(expressionCoeff1, expressionCoeff2)); }
		public static Expression operator -(Expression expressionCoeff1, Expression expressionCoeff2) { return (Add(expressionCoeff1, Negate(expressionCoeff2))); }


		public static bool operator ==(Expression expressionCoeff1, Expression expressionCoeff2) { return expressionCoeff1.Equals(expressionCoeff2); }
		public static bool operator !=(Expression expressionCoeff1, Expression expressionCoeff2) { return (!expressionCoeff1.Equals(expressionCoeff2)); }

	}

}
