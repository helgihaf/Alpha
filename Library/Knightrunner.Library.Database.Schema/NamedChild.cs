using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Knightrunner.Library.Database.Schema
{
    public class NamedChild 
    {
        private string name;
        private IParent parent;

        public string Name
        {
            get { return name; }

            set
            {
                if (name == value)
                {
                    return;
                }

                if (!OnNameChanging(name, value))
                {
                    throw new ArgumentException("Invalid property value");
                }
                var oldValue = name;
                name = value;
                OnNameChanged(oldValue, name);
            }
        }


        internal IParent Parent
        {
            get { return parent; }
            set { parent = value; }
            //set
            //{
            //    if (object.ReferenceEquals(parent, value))
            //    {
            //        return;
            //    }

            //    if (!OnParentChanging(parent, value))
            //    {
            //        throw new ArgumentException("Invalid property value");
            //    }
            //    var oldValue = parent;
            //    parent = value;
            //    OnParentChanged(oldValue, parent);
            //}
        }


        public event EventHandler<PropertyChangingEventArgs<string>> NameChanging;
        public event EventHandler<PropertyChangedEventArgs<string>> NameChanged;

        //public event EventHandler<PropertyChangingEventArgs<IParent>> ParentChanging;
        //public event EventHandler<PropertyChangedEventArgs<IParent>> ParentChanged;


        protected virtual bool OnNameChanging(string currentValue, string newValue)
        {
            bool result = true;
            if (NameChanging != null)
            {
                var e = new PropertyChangingEventArgs<string> { OldValue = currentValue, NewValue = newValue };
                NameChanging(this, e);
                result = !e.Cancel;
            }

            return result;
        }

        protected virtual void OnNameChanged(string oldValue, string currentValue)
        {
            if (NameChanged != null)
            {
                var e = new PropertyChangedEventArgs<string> { OldValue = oldValue, NewValue = currentValue };
                NameChanged(this, e);
            }
        }


        //private bool OnParentChanging(IParent currentValue, IParent newValue)
        //{
        //    bool result = true;
        //    if (ParentChanging != null)
        //    {
        //        var e = new PropertyChangingEventArgs<IParent> { OldValue = currentValue, NewValue = newValue };
        //        ParentChanging(this, e);
        //        result = !e.Cancel;
        //    }

        //    return result;
        //}

        //private void OnParentChanged(IParent oldValue, IParent currentValue)
        //{
        //    if (ParentChanged != null)
        //    {
        //        var e = new PropertyChangedEventArgs<IParent> { OldValue = oldValue, NewValue = currentValue };
        //        ParentChanged(this, e);
        //    }
        //}


    }



}
