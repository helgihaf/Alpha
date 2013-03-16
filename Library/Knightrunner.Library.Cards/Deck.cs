using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Cards
{
    public class Deck
    {
        private List<Card> cards = new List<Card>();
        private Random random = new Random();

        public Deck()
        {
            foreach (Card card in Card.GetAllCards())
            {
                cards.Add(card);
            }
        }


        public Card PickCard()
        {
            Card result = null;

            if (cards.Count > 0)
            {
                int index = 0;
                result = cards[index];
                cards.RemoveAt(index);
            }

            return result;
        }

        public int Count
        {
            get
            {
                return cards.Count;
            }
        }


        public void Shuffle()
        {
            List<Card> newCards = new List<Card>();
            while (cards.Count > 0)
            {
                int index = random.Next(cards.Count);
                newCards.Add(cards[index]);
                cards.RemoveAt(index);
            }

            cards = newCards;
        }

        public void Add(Card card)
        {
            cards.Add(card);
        }

        public void Add(IEnumerable<Card> cards)
        {
            this.cards.AddRange(cards);
        }

    }
}
