using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema
{
    public class AssociationProperty
    {
        public AssociationProperty()
        {
            Child = new AssociationEndPoint();
            Parent = new AssociationEndPoint();
        }

        public AssociationEndPoint Child { get; set; }
        public AssociationEndPoint Parent { get; set; }
    }


    public class AssociationEndPoint
    {
        public AssociationEndPoint()
        {
            Access = PropertyAccess.Public;
            InheritanceModifier = PropertyInheritanceModifier.None;
        }

        public PropertyAccess Access { get; set; }
        public PropertyInheritanceModifier InheritanceModifier { get; set; }
        public string Name { get; set; }
    }


    public enum PropertyAccess
    {
        Internal,
        Public
    }


    public enum PropertyInheritanceModifier
    {
        None,
        New,
        NewVirtual,
        Virtual,
        Override
    }


}
