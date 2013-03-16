using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Cards
{
    public class Card : IComparable<Card>, IEquatable<Card>
    {
        public const int CardsInSuite = 13;

        public Card(Suite suite, int number)
        {
            this.Suite = suite;
            this.Number = number;
        }

        public Card(int serialValue)
        {
            this.Suite = (Suite)(serialValue / CardsInSuite);
            this.Number = serialValue % CardsInSuite;
        }

        public Suite Suite { get; private set; }

        /// <summary>
        /// 0-12 where 0 is 2, and 12 is Ace.
        /// </summary>
        public int Number { get; private set; }

        public int SerialValue
        {
            get
            {
                return (int)Suite * CardsInSuite + Number;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals((Card)obj);
        }

        public override int GetHashCode()
        {
            return SerialValue;
        }

        
        public int CompareTo(Card other)
        {
            int thisSerialValue = SerialValue;
            int otherSerialValue = other.SerialValue;

            return thisSerialValue.CompareTo(otherSerialValue);
        }

        public bool Equals(Card other)
        {
            return CompareTo(other) == 0;
        }


        public override string ToString()
        {
            return SuiteToString(Suite) + CardNumberToString(Number);
        }

        public static string CardNumberToString(int number)
        {
            switch (number)
            {
                case 9:
                    return "J";
                case 10:
                    return "Q";
                case 11:
                    return "K";
                case 12:
                    return "A";
                default:
                    return (number + 2).ToString();
            }
        }

        public static string SuiteToString(Cards.Suite suite)
        {
            switch (suite)
	        {
                case Suite.Heart:
                    return "H";
                case Suite.Spade:
                    return "S";
                case Suite.Diamond:
                    return "D";
                case Suite.Club:
                    return "C";
                default:
                    throw new ArgumentException("Unknown value for suite");
	        }
        }


        public static IEnumerable<Card> GetAllCards()
        {
            for (int i = 0; i < 52; i++)
            {
                yield return new Card(i);
            }
        }
    }
}
