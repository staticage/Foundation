using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Foundation.Core
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (var item in @this)
            {
                action(item);
            }
        }

        public static bool IsNullOrEmpty(this IEnumerable @this)
        {
            return @this == null || @this.OfType<object>().None();
        }
        
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return @this == null || @this.None();
        }

        public static bool None<TSource>(this IEnumerable<TSource> @this) => !@this.Any();
        public static bool None<TSource>(this IEnumerable<TSource> @this, Func<TSource, bool> predicate) => !@this.Any(predicate);
        public static string JoinString<TSource>(this IEnumerable<TSource> @this, string separator) => string.Join(separator, @this);

        public static Tuple<IEnumerable<TSource>,IEnumerable<TSource>> Partition<TSource>(this IEnumerable<TSource> @this, Func<TSource, bool> predicate)
        {
            return new Tuple<IEnumerable<TSource>, IEnumerable<TSource>>(@this.Where(predicate), @this.Where(x=> !predicate(x)));
        }
        
        public static void Partition<TSource>(this IEnumerable<TSource> @this, Func<TSource, bool> predicate, Action<TSource> trueAction,Action<TSource> falseAction)
        {
            var result = @this.Partition(predicate);
            result.Item1.ForEach(trueAction);
            result.Item2.ForEach(falseAction);
        }
        
        public static void Partition<TSource>(this IEnumerable<TSource> @this, IEnumerable<TSource> another, Action<TSource> newItemAction,Action<TSource> deletedItemAction)
        {
            var newItems = @this.Except(another);
            var deletedItems = another.Except(@this);
            newItems.ForEach(newItemAction);
            deletedItems.ForEach(deletedItemAction);
        }
        
        public static void RemoveWhere<TSource>(this IList<TSource> @this, Func<TSource, bool> predicate)
        {
            var items = @this.Where(predicate).ToArray();
            foreach (var item in items)
            {
                @this.Remove(item);
            }
        }
    }
}
