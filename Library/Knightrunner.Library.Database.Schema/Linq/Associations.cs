using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Knightrunner.Library.Database.Schema.Linq
{
    internal class Associations
    {
        private HashSet<string> names = new HashSet<string>();
        private Dictionary<string, List<Association>> fromTableAssociations = new Dictionary<string, List<Association>>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, List<Association>> toTableAssociations = new Dictionary<string, List<Association>>(StringComparer.OrdinalIgnoreCase);

        // dictionary to store all member names of a table type - ensures we don't create duplicate member names
        private Dictionary<string, HashSet<string>> typesAndMembers = new Dictionary<string, HashSet<string>>();

        internal void Add(Table table, ForeignKey foreignKey)
        {
            Association fromAssociation = new Association();
            AssociationProperty associationProperty = foreignKey.AssociationProperty;
            if (associationProperty == null)
            {
                // Create the default association property
                associationProperty = new AssociationProperty();
            }

            // Figure out the assocation name
            bool isUnique = false;
            int count = 0;
            while (!isUnique)
            {
                fromAssociation.Name =
                    string.Format(CultureInfo.InvariantCulture, "{0}_{1}{2}",
                    NameHelper.GetTypeName(foreignKey.ToTable),
                    NameHelper.GetTypeName(table),
                    count > 0 ? count.ToString(System.Globalization.CultureInfo.InvariantCulture) : string.Empty);
                isUnique = !names.Contains(fromAssociation.Name);
                count++;
            }
            names.Add(fromAssociation.Name);

            string baseName = GetMemberName(table, foreignKey.ToTable, false);

            // Set FROM table association properties
            if (associationProperty.Parent.Name == null)
            {
                // Automatically create the name
                fromAssociation.Member = baseName;
            }
            else
            {
                fromAssociation.Member = associationProperty.Parent.Name;
                //fromAssociation.Storage = "_" + baseName;
                var members = GetMembersOfTable(table);
                members.Add(fromAssociation.Member);
            }
            //  AccessModifier="Internal"  Modifier="NewVirtual"

            fromAssociation.ThisKey = foreignKey.FromColumnsToString();
            fromAssociation.OtherKey = foreignKey.ToColumnsToString();

            fromAssociation.Type = NameHelper.GetTypeName(foreignKey.ToTable);
            fromAssociation.IsForeignKey = true;
            fromAssociation.AccessModifier = associationProperty.Parent.Access;
            fromAssociation.Modifier = associationProperty.Parent.InheritanceModifier;
            AddAssociation(table.Name, fromTableAssociations, fromAssociation);

            if (associationProperty.Child != null)
            {
                // Create TO table association
                Association toAssociation = new Association();
                toAssociation.Name = fromAssociation.Name;

                baseName = GetMemberName(foreignKey.ToTable, table, true);
                if (associationProperty.Child.Name == null)
                {
                    // Automatically create the name
                    toAssociation.Member = baseName;
                }
                else
                {
                    toAssociation.Member = associationProperty.Child.Name;
                    //toAssociation.Storage = "_" + baseName;
                    var members = GetMembersOfTable(foreignKey.ToTable);
                    members.Add(toAssociation.Member);
                }

                toAssociation.ThisKey = foreignKey.ToColumnsToString();
                toAssociation.OtherKey = foreignKey.FromColumnsToString();

                toAssociation.Type = NameHelper.GetTypeName(table);
                toAssociation.AccessModifier = associationProperty.Child.Access;
                toAssociation.Modifier = associationProperty.Child.InheritanceModifier;
                AddAssociation(foreignKey.ToTable.Name, toTableAssociations, toAssociation);
            }
        }

        private void AddAssociation(string tableName, Dictionary<string, List<Association>> dictionary, Association association)
        {
            List<Association> list;
            if (!dictionary.TryGetValue(tableName, out list))
            {
                list = new List<Association>();
                dictionary.Add(tableName, list);
            }
            list.Add(association);
        }

        internal IEnumerable<Association> GetForFromTable(string tableName)
        {
            List<Association> result;
            if (!fromTableAssociations.TryGetValue(tableName, out result))
            {
                result = new List<Association>();
            }

            return result;
        }

        internal IEnumerable<Association> GetForToTable(string tableName)
        {
            List<Association> result;
            if (!toTableAssociations.TryGetValue(tableName, out result))
            {
                result = new List<Association>();
            }

            return result;
        }


        private string GetMemberName(Table fromTable, Table toTable, bool plural)
        {
            // Prepare for creating a member name
            var members = GetMembersOfTable(fromTable);
            string baseMemberName;
            if (plural)
                baseMemberName = NameHelper.GetMemberNamePlural(toTable);
            else
                baseMemberName = NameHelper.GetMemberNameSingular(toTable);
            string currentSuggestion = baseMemberName;

            int tries = 0;
            while (members.Contains(currentSuggestion))
            {
                tries++;
                currentSuggestion = baseMemberName + tries.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            members.Add(currentSuggestion);

            return currentSuggestion;
        }


        private HashSet<string> GetMembersOfTable(Table table)
        {
            HashSet<string> members;
            if (!typesAndMembers.TryGetValue(table.Name, out members))
            {
                members = new HashSet<string>();
                // Add all existing members
                foreach (Column column in table.Columns)
                {
                    members.Add(column.Name);
                }
                // Add the type name itself
                members.Add(NameHelper.GetTypeName(table));
                typesAndMembers.Add(table.Name, members);
            }

            return members;
        }

    }
}
