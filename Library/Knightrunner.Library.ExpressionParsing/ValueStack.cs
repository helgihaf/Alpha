using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing
{
    internal class ValueStack
    {
        private Stack<object> stack = new Stack<object>();

        public void Clear()
        {
            stack.Clear();
        }

        public void Push(object value)
        {
            stack.Push(value);
        }

        public void Or()
        {
            bool b = Convert.ToBoolean(stack.Pop());
            bool a = Convert.ToBoolean(stack.Pop());
            stack.Push(a || b);
        }

        public void And()
        {
            bool b = Convert.ToBoolean(stack.Pop());
            bool a = Convert.ToBoolean(stack.Pop());
            stack.Push(a && b);
        }

        public void Not()
        {
            bool a = Convert.ToBoolean(stack.Pop());
            stack.Push(!a);
        }


        public void BitwiseOr()
        {
            int b = Convert.ToInt32(stack.Pop());
            int a = Convert.ToInt32(stack.Pop());
            stack.Push(a | b);
        }

        public void BitwiseAnd()
        {
            int b = Convert.ToInt32(stack.Pop());
            int a = Convert.ToInt32(stack.Pop());
            stack.Push(a & b);
        }

        public void BitwiseNot()
        {
            int a = Convert.ToInt32(stack.Pop());
            stack.Push(~a);
        }

        public void IsEqual()
        {
            object b = stack.Pop();
            object a = stack.Pop();
            stack.Push(object.Equals(a, b));
        }

        public void IsNotEqual()
        {
            object b = stack.Pop();
            object a = stack.Pop();
            stack.Push(!object.Equals(a, b));
        }

        public void IsLess()
        {
            object b = stack.Pop();
            object a = stack.Pop();

            ExtendTypes(ref a, ref b);

            if (a is int && b is int)
            {
                stack.Push((int)a < (int)b);
            }
            else if (a is double && b is double)
            {
                stack.Push((double)a < (double)b);
            }
            else
            {
                stack.Push(string.Compare((string)a, (string)b) < 0);
            }
        }


        public void IsGreater()
        {
            object b = stack.Pop();
            object a = stack.Pop();

            ExtendTypes(ref a, ref b);

            if (a is int && b is int)
            {
                stack.Push((int)a > (int)b);
            }
            else if (a is double && b is double)
            {
                stack.Push((double)a > (double)b);
            }
            else
            {
                stack.Push(string.Compare((string)a, (string)b) > 0);
            }
        }

        public void IsLessOrEqual()
        {
            object b = stack.Pop();
            object a = stack.Pop();

            ExtendTypes(ref a, ref b);

            if (a is int && b is int)
            {
                stack.Push((int)a <= (int)b);
            }
            else if (a is double && b is double)
            {
                stack.Push((double)a <= (double)b);
            }
            else
            {
                stack.Push(string.Compare((string)a, (string)b) <= 0);
            }
        }

        public void IsGreaterOrEqual()
        {
            object b = stack.Pop();
            object a = stack.Pop();

            ExtendTypes(ref a, ref b);

            if (a is int && b is int)
            {
                stack.Push((int)a >= (int)b);
            }
            else if (a is double && b is double)
            {
                stack.Push((double)a >= (double)b);
            }
            else
            {
                stack.Push(string.Compare((string)a, (string)b) >= 0);
            }
        }

        public void Add()
        {
            object b = stack.Pop();
            object a = stack.Pop();

            ExtendTypes(ref a, ref b);

            if (a is int && b is int)
            {
                stack.Push((int)a + (int)b);
            }
            else if (a is double && b is double)
            {
                stack.Push((double)a + (double)b);
            }
            else
            {
                stack.Push(string.Concat((string)a, (string)b));
            }
        }

        public void Subtract()
        {
            object b = stack.Pop();
            object a = stack.Pop();

            ExtendTypes(ref a, ref b);

            if (a is int && b is int)
            {
                stack.Push((int)a - (int)b);
            }
            else if (a is double && b is double)
            {
                stack.Push((double)a - (double)b);
            }
            else
            {
                if (a == null)
                {
                    throw new EvaluationException("Cannot subtract a value from a null value");
                }
                if (b != null)
                {
                    stack.Push(((string)a).Replace((string)b, string.Empty));
                }
            }
        }

        public void Multiply()
        {
            object b = stack.Pop();
            object a = stack.Pop();

            ExtendTypes(ref a, ref b);

            if (a is int && b is int)
            {
                stack.Push((int)a * (int)b);
            }
            else if (a is double && b is double)
            {
                stack.Push((double)a * (double)b);
            }
            else
            {
                throw new EvaluationException("Cannot multiply string values");
            }
        }

        public void Divide()
        {
            object b = stack.Pop();
            object a = stack.Pop();

            ExtendTypes(ref a, ref b);

            if (a is int && b is int)
            {
                stack.Push((int)a / (int)b);
            }
            else if (a is double && b is double)
            {
                stack.Push((double)a / (double)b);
            }
            else
            {
                throw new EvaluationException("Cannot divide string values");
            }
        }

        public void Modulus()
        {
            object b = stack.Pop();
            object a = stack.Pop();

            ExtendTypes(ref a, ref b);

            if (a is int && b is int)
            {
                stack.Push((int)a % (int)b);
            }
            else if (a is double && b is double)
            {
                stack.Push((double)a % (double)b);
            }
            else
            {
                throw new EvaluationException("Cannot use modulus on string values");
            }
        }

        public void Power()
        {
            object b = stack.Pop();
            object a = stack.Pop();

            ExtendTypes(ref a, ref b);

            if (a is int && b is int)
            {
                stack.Push(Convert.ToInt32(Math.Pow((int)a, (int)b)));
            }
            else if (a is double && b is double)
            {
                stack.Push(Math.Pow((double)a, (double)b));
            }
            else
            {
                throw new EvaluationException("Cannot use power on string values");
            }
        }



        private void ExtendTypes(ref object a, ref object b)
        {
            // int vs. double:      convert int to double
            // int vs. string:      convert int to string
            // double vs. string:   convert double to string

            EvaluationType aType = GetEvaluationTypeOf(a);
            EvaluationType bType = GetEvaluationTypeOf(b);

            if (aType == EvaluationType.Object || bType == EvaluationType.Object)
            {
                throw new EvaluationException("Cannot extend types of object types");
            }

            if (aType == EvaluationType.Int)
            {
                if (bType == EvaluationType.Double)
                {
                    a = Convert.ToDouble(a);
                }
                else if (bType == EvaluationType.String)
                {
                    a = ((int)a).ToString();
                }
            }
            else if (aType == EvaluationType.Double)
            {
                if (bType == EvaluationType.Int)
                {
                    b = Convert.ToDouble(b);
                }
                else if (bType == EvaluationType.String)
                {
                    a = ((double)a).ToString();
                }
            }
            else if (aType == EvaluationType.String)
            {
                if (bType == EvaluationType.Int)
                {
                    b = ((int)b).ToString();
                }
                else if (bType == EvaluationType.Double)
                {
                    b = ((double)b).ToString();
                }
            }
        }

        private EvaluationType GetEvaluationTypeOf(object a)
        {
            EvaluationType result;

            if (a is string)
            {
                result = EvaluationType.String;
            }
            else if (a is int)
            {
                result = EvaluationType.Int;
            }
            else if (a is double)
            {
                result = EvaluationType.Double;
            }
            else if (a == null || !a.GetType().IsValueType)
            {
                result = EvaluationType.Object;
            }
            else
            {
                throw new ArgumentException("Invalid data type of a: " + a.GetType().FullName);
            }

            return result;
        }

        public object Peek()
        {
            return stack.Peek();
        }

        public object Pop()
        {
            return stack.Pop();
        }


    }


    [Serializable]
    public class EvaluationException : Exception
    {
        public EvaluationException() { }
        public EvaluationException(string message) : base(message) { }
        public EvaluationException(string message, Exception inner) : base(message, inner) { }
        protected EvaluationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

}
