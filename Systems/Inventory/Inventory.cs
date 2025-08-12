using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldOfSkyfire
{
    public class Inventory<T>
    {
        private readonly List<T> items = [];

        public event Action<T>? ItemAdded;
        public event Action<T>? ItemRemoved;

        public int Count => items.Count;
        public IReadOnlyList<T> Items => items.AsReadOnly();

        public void AddItem(T item)
        {
            items.Add(item);
            ItemAdded?.Invoke(item);
        }

        public void RemoveItem(T item)
        {
            items.Remove(item);
            ItemRemoved?.Invoke(item);
        }

        public override string ToString() => "Inventory";
    }
}
