using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace Knightrunner.WorkTrack.Wpf
{
    public class BusyScope : IDisposable
    {
        private Cursor cursor;
        private FrameworkElement frameworkElement;
        private List<UIElement> disabledUIElements;

        public BusyScope(FrameworkElement frameworkElement, params UIElement[] uiElements)
        {
            this.cursor = frameworkElement.Cursor;
            frameworkElement.Cursor = Cursors.Wait;
            this.frameworkElement = frameworkElement;

            if (uiElements != null)
            {
                disabledUIElements = new List<UIElement>();
                foreach (var uiElement in uiElements)
                {
                    if (uiElement.IsEnabled)
                    {
                        uiElement.IsEnabled = false;
                        disabledUIElements.Add(uiElement);
                    }
                }
            }
        }

        public void Dispose()
        {
            if (frameworkElement != null)
            {
                if (disabledUIElements != null)
                {
                    foreach (var uiElement in disabledUIElements)
                    {
                        uiElement.IsEnabled = true;
                    }
                    disabledUIElements.Clear();
                    disabledUIElements = null;
                }
                frameworkElement.Cursor = cursor;
                cursor = null;
                frameworkElement = null;
                GC.SuppressFinalize(this);
            }
        }
    }
}
