using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.WorkTrack2013.Database
{
    public static class Converters
    {
        public static readonly Converter<Entities.Project, Contract.Project> ProjectToContract =
            delegate(Entities.Project data)
            {
                if (data == null)
                {
                    return null;
                }

                var conv = new Contract.Project();

                conv.PublicId = data.PublicId;
                conv.Text = data.Text;
                conv.Active = data.Active;

                return conv;
            };

    }
}
