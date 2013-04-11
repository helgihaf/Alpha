using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.WorkTrack.Database
{
    public abstract partial class LinqEntityBase
    {
        public abstract void Detach();

        protected static System.Data.Linq.EntitySet<TEntity> Detach<TEntity>(System.Data.Linq.EntitySet<TEntity> set, Action<TEntity> onAdd, Action<TEntity> onRemove)
            where TEntity : LinqEntityBase
        {
            if (set == null || !set.HasLoadedOrAssignedValues)
            {
                return new System.Data.Linq.EntitySet<TEntity>(onAdd, onRemove);
            }

            // copy list and detach all entities
            var list = set.ToList();
            list.ForEach(t => t.Detach());

            var newSet = new System.Data.Linq.EntitySet<TEntity>(onAdd, onRemove);
            newSet.Assign(list);
            return newSet;
        }


        protected static System.Data.Linq.EntityRef<TEntity> Detach<TEntity>(System.Data.Linq.EntityRef<TEntity> entity)
            where TEntity : LinqEntityBase
        {
            if (!entity.HasLoadedOrAssignedValue || entity.Entity == null)
            {
                return new System.Data.Linq.EntityRef<TEntity>();
            }
            entity.Entity.Detach();
            return new System.Data.Linq.EntityRef<TEntity>(entity.Entity);
        }


    }
}
