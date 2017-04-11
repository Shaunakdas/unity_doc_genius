using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace UIMath{
	public struct MathOperator:IExpressionItem{
		string _symbol;
		public string Symbol
		{
			get { return _symbol; }
			private set { _symbol = value; }
		}
		public enum Operation {Addition,Multiplication,Division,Subtraction,RoundBracketStart,RoundBracketEnd,
			SquareBracketStart,SquareBracketEnd,CurlyBracketStart,CurlyBracketEnd,LessThan,GreaterThan,
			Percent
			}
		Operation _mathOperation;
		public Operation MathOperation
		{
			get { return _mathOperation; }
			private set { _mathOperation = value; }
		}
		public MathOperator(string symbol)
		{
			_symbol = symbol;
			switch (symbol) {
			case("+"):
				_mathOperation = Operation.Addition;
				break;
			case("-"):
				_mathOperation = Operation.Subtraction;
				break;
			case("//div"):
				_mathOperation = Operation.Division;
				break;
			case("//times"):
				_mathOperation = Operation.Multiplication;
				break;
			case("("):
				_mathOperation = Operation.RoundBracketStart;
				break;
			case(")"):
				_mathOperation = Operation.RoundBracketEnd;
				break;
			case("{"):
				_mathOperation = Operation.CurlyBracketStart;
				break;
			case("}"):
				_mathOperation = Operation.CurlyBracketEnd;
				break;
			case("["):
				_mathOperation = Operation.SquareBracketStart;
				break;
			case("]"):
				_mathOperation = Operation.SquareBracketEnd;
				break;
			case("<"):
				_mathOperation = Operation.LessThan;
				break;
			case(">"):
				_mathOperation = Operation.GreaterThan;
				break;
			case("%"):
				_mathOperation = Operation.Percent;
				break;
			default:
				_mathOperation = Operation.Addition;
				break;
			}
		}

		public bool Equals(MathOperator other)
		{
			if (other == null)
				return false;

			return (Symbol==other.Symbol);
		}
		public bool Equals(object obj)
		{
			if (obj == null || !(obj is MathOperator))
				return false;

			return Equals((MathOperator)obj);
		}
		public string ToLatexString()
		{
			string latex = "";
			switch (_mathOperation) {
			case(Operation.Addition):
				return "+";
			case(Operation.Multiplication):
				return "\\times";
			case(Operation.Division):
				return "\\div";
			case(Operation.Subtraction):
				return "-";
			case(Operation.RoundBracketStart):
				return "(";
			case(Operation.RoundBracketEnd):
				return ")";
			case(Operation.CurlyBracketStart):
				return "{";
			case(Operation.CurlyBracketEnd):
				return "}";
			case(Operation.SquareBracketStart):
				return "[";
			case(Operation.SquareBracketEnd):
				return "]";
			case(Operation.LessThan):
				return "<";
			case(Operation.GreaterThan):
				return ">";
			case(Operation.Percent):
				return "\\%";
			default:
				return "+";
			}
		}
		public long Value(List<long> value)
		{
			return (long)0;
		}
		public long Value()
		{
			return (long)0;
		}
		public List<string> GetVariableList()
		{
			return null;
		}
		public static bool operator ==(MathOperator op1, MathOperator op2) { return op1.Equals(op2); }
		public static bool operator !=(MathOperator op1, MathOperator op2) { return (!op1.Equals(op2)); }


	}
}
