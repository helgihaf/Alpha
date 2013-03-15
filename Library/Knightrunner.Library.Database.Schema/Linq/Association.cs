using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Knightrunner.Library.Database.Schema.Linq
{
    internal class Association
    {
        public string Name { get; set; }
        public string Member { get; set; }
        public string Storage { get; set; }
        public string ThisKey { get; set; }
        public string OtherKey { get; set; }
        public string Type { get; set; }
        public bool IsForeignKey { get; set; }
        public PropertyAccess AccessModifier { get; set; }
        public PropertyInheritanceModifier Modifier { get; set; }

        public XElement CreateXElement(string xmlNamespace)
        {
            var association = new XElement(XName.Get("Association", xmlNamespace));
            association.Add(new XAttribute("Name", Name));
            association.Add(new XAttribute("Member", Member));
            if (Storage != null)
                association.Add(new XAttribute("Storage", Storage));
            if (AccessModifier != PropertyAccess.Public)
                association.Add(new XAttribute("AccessModifier", AccessModifier.ToString()));
            if (Modifier != PropertyInheritanceModifier.None)
                association.Add(new XAttribute("Modifier", Modifier.ToString()));
            association.Add(new XAttribute("ThisKey", ThisKey));
            association.Add(new XAttribute("OtherKey", OtherKey));
            association.Add(new XAttribute("Type", Type));
            if (IsForeignKey)
                association.Add(new XAttribute("IsForeignKey", "true"));

            return association;
        }


    }
}
