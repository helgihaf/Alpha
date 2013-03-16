using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Controls.PageSelection
{
    public interface ISelector
    {
        void Initialize(IDirector director);

        IDirector Director { get; }
    }
}
