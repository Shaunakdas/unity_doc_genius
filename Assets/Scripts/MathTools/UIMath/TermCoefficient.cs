using UnityEngine;
using System.Collections;
using System;
namespace UIMath{
	public struct TermCoefficient {
		long _base;
		public long Base
		{
			get { return _base; }
			private set { _base = value; }
		}
		long _exponent;
		public long Exponent
		{
			get { return _exponent; }
			private set { _exponent = value; }
		}
		public TermCoefficient(long value)
		{
			_base = value;
			_exponent = 1;
		}
		public TermCoefficient(long baseNumber, long exponent)
		{
			_base  = baseNumber;
			_exponent = exponent;
		}
		public bool Equals(TermCoefficient other)
		{
			if (other == null)
				return false;

			return (Value() ==other.Value());
		}
		public bool Equals(object obj)
		{
			if (obj == null || !(obj is TermCoefficient))
				return false;

			return Equals((TermCoefficient)obj);
		}
		public long Value()
		{
			return (long)Math.Pow (Base, Exponent);
		}
		public string ToLatexString()
		{

			string sb = "";
			sb+= this.Base.ToString();
			sb+= "^{";
			sb+= this.Exponent.ToString();
			sb+= "}";

			return sb;
		}
		private static TermCoefficient Add(TermCoefficient termCoeff1, TermCoefficient termCoeff2)
		{
			try
			{
				checked
				{
					long iBase = termCoeff1.Value() + termCoeff2.Value();
					long iExponent = 1;
					return new TermCoefficient(iBase, iExponent);
				}
			}
			catch (OverflowException e)
			{
				throw new OverflowException("Overflow occurred while performing arithemetic operation on TermCoefficient.", e);
			}
			catch (Exception e)
			{
				throw new Exception("An error occurred while performing arithemetic operation on TermCoefficient.", e);
			}
		}
		private static TermCoefficient Multiply(TermCoefficient termCoeff1, TermCoefficient termCoeff2)
		{
			try
			{
				checked
				{
					long iBase = termCoeff1.Value() * termCoeff2.Value();
					long iExponent = 1;
					return new TermCoefficient(iBase, iExponent);
				}
			}
			catch (OverflowException e)
			{
				throw new OverflowException("Overflow occurred while performing arithemetic operation on TermCoefficient.", e);
			}
			catch (Exception e)
			{
				throw new Exception("An error occurred while performing arithemetic operation on TermCoefficient.", e);
			}
		}

		public static TermCoefficient operator +(TermCoefficient termCoeff1, TermCoefficient termCoeff2) { return (Add(termCoeff1, termCoeff2)); }
		public static TermCoefficient operator *(TermCoefficient termCoeff1, TermCoefficient termCoeff2) { return (Multiply(termCoeff1, termCoeff2)); }


		public static bool operator ==(TermCoefficient termCoeff1, TermCoefficient termCoeff2) { return termCoeff1.Equals(termCoeff2); }
		public static bool operator !=(TermCoefficient termCoeff1, TermCoefficient termCoeff2) { return (!termCoeff1.Equals(termCoeff2)); }
		public static bool operator <(TermCoefficient termCoeff1, TermCoefficient termCoeff2) { return termCoeff1.Value() < termCoeff2.Value(); }
		public static bool operator >(TermCoefficient termCoeff1, TermCoefficient termCoeff2) { return termCoeff1.Value() > termCoeff2.Value(); }
		public static bool operator <=(TermCoefficient termCoeff1, TermCoefficient termCoeff2) { return termCoeff1.Value() <= termCoeff2.Value(); }
		public static bool operator >=(TermCoefficient termCoeff1, TermCoefficient termCoeff2) { return termCoeff1.Value() >= termCoeff2.Value(); }
		public static explicit operator double(TermCoefficient termCoeff)
		{
			return ((double)termCoeff.Value());
		}
	}

}
