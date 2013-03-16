using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Knightrunner.Library.Quotes
{
    public class QuoteStore : IEnumerable<Quote>
    {
        public List<Quote> quotes = new List<Quote>();
        public List<int> remainingIndexes = null;
        public Random random = new Random();

        public QuoteStore()
        {
        }

        public QuoteStore(string filePath)
        {
            LoadFromFile(filePath);
        }

        public void Clear()
        {
            quotes.Clear();
            remainingIndexes = null;
        }


        private void LoadFromFile(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException("filePath");

            if (!File.Exists(filePath))
                throw new ArgumentException("File does not exist");

            Quote[] quoteArray;
            XmlSerializer serializer = new XmlSerializer(typeof(Quote[]));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                quoteArray = (Quote[])serializer.Deserialize(fileStream);
            }

            quotes.AddRange(quoteArray);
            remainingIndexes = null;
        }


        public Quote GetQuote()
        {
            return GetQuote(true);
        }

        public Quote GetQuote(bool remember)
        {
            int quoteIndex;

            if (remember)
            {
                if (remainingIndexes == null || remainingIndexes.Count == 0)
                {
                    remainingIndexes = new List<int>();
                    for (int i = 0; i < quotes.Count; i++)
                    {
                        remainingIndexes.Add(i);
                    }
                }

                quoteIndex = random.Next(remainingIndexes.Count);
                remainingIndexes.Remove(quoteIndex);
            }
            else
            {
                quoteIndex = random.Next(quotes.Count);
            }

            return quotes[quoteIndex];
        }


        public IEnumerator<Quote> GetEnumerator()
        {
            return quotes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (System.Collections.IEnumerator)quotes.GetEnumerator();
        }
    }
}
