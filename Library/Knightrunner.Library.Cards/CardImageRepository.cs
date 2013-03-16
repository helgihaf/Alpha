using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Knightrunner.Library.Cards
{
    public class CardImageRepository : IDisposable
    {
        private Dictionary<int, Image> images = new Dictionary<int, Image>();
        private Image backImage;

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (images != null)
                {
                    foreach (var image in images.Values)
                    {
                        image.Dispose();
                    }
                    images = null;
                }

                if (backImage != null)
                {
                    backImage.Dispose();
                    backImage = null;
                }
            }
        }

        public Image GetCardImage(Card card)
        {
            Image image;

            if (!images.TryGetValue(card.SerialValue, out image))
            {
                image = LoadImage(card);
                images.Add(card.SerialValue, image);
            }

            return image;
        }

        public Image GetBackImage()
        {
            if (backImage == null)
            {
                backImage = Image.FromFile("back-red-150-1.png");
            }
            return backImage;
        }

        private Image LoadImage(Card card)
        {
            string fileName = GetCardFileName(card);
            return Image.FromFile(fileName);
        }

        private string GetCardFileName(Card card)
        {
            return Enum.GetName(typeof(Suite), card.Suite).ToLower() + "s-" + Card.SuiteToString(card.Suite).ToLower() + "-150.png";
        }



    }
}
