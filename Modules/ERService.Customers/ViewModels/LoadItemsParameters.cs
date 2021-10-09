using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ERService.Customers.ViewModels
{
    public struct LoadItemsParameters<TSource>
    {
        public uint Count;
        public int BaseIndex;
        public Expression<Func<TSource, bool>> FilterPredicate;

        public LoadItemsParameters(uint count, int baseIndex, Expression<Func<TSource, bool>> filterPredicate)
        {
            Count = count;
            BaseIndex = baseIndex;
            FilterPredicate = filterPredicate;
        }

        public override bool Equals(object obj)
        {
            return obj is LoadItemsParameters<TSource> other &&
                   Count == other.Count &&
                   BaseIndex == other.BaseIndex &&
                   EqualityComparer<Expression<Func<TSource, bool>>>.Default.Equals(FilterPredicate, other.FilterPredicate);
        }

        public override int GetHashCode() => HashCode.Combine(Count, BaseIndex, FilterPredicate);

        public void Deconstruct(out uint count, out int baseIndex, out Expression<Func<TSource, bool>> filterPredicate)
        {
            count = Count;
            baseIndex = BaseIndex;
            filterPredicate = FilterPredicate;
        }

        public static implicit operator (uint Count, int BaseIndex, Expression<Func<TSource, bool>> FilterPredicate)(LoadItemsParameters<TSource> value) => 
            (value.Count, value.BaseIndex, value.FilterPredicate);

        public static implicit operator LoadItemsParameters<TSource>((uint Count, int BaseIndex, Expression<Func<TSource, bool>> FilterPredicate) value) => 
            new LoadItemsParameters<TSource>(value.Count, value.BaseIndex, value.FilterPredicate);
    }
}