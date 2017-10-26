﻿using System.Collections.Generic;

namespace MesPremiersUnitTestProject
{
    /// <summary>
    /// Represents a collection of keys and values.
    /// Multiple values can have the same key.
    /// </summary>
    /// <typeparam name="TKey">Type of the keys.</typeparam>
    /// <typeparam name="TValue">Type of the values.</typeparam>
    public class MultiMapSet<TKey, TValue> : Dictionary<TKey, HashSet<TValue>>
    {
        public MultiMapSet() : base()
        {
        }

        public MultiMapSet(int capacity) : base(capacity)
        {
        }

        /// <summary>
        /// Adds an element with the specified key and value into the MultiMap. 
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        public void Add(TKey key, TValue value)
        {
            HashSet<TValue> valueList;

            if (TryGetValue(key, out valueList))
            {
                valueList.Add(value);
            }
            else
            {
                valueList = new HashSet<TValue> {value};
                Add(key, valueList);
            }
        }

        /// <summary>
        /// Removes first occurence of an element with a specified key and value.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <param name="value">The value of the element to remove.</param>
        /// <returns>true if the an element is removed;
        /// false if the key or the value were not found.</returns>
        public bool Remove(TKey key, TValue value)
        {
            HashSet<TValue> valueList;

            if (TryGetValue(key, out valueList))
            {
                if (valueList.Remove(value))
                {
                    if (valueList.Count == 0)
                    {
                        Remove(key);
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes all occurences of elements with a specified key and value.
        /// </summary>
        /// <param name="key">The key of the elements to remove.</param>
        /// <param name="value">The value of the elements to remove.</param>
        /// <returns>Number of elements removed.</returns>
        public int RemoveAll(TKey key, TValue value)
        {
            HashSet<TValue> valueList;
            int n = 0;

            if (TryGetValue(key, out valueList))
            {
                while (valueList.Remove(value))
                {
                    n++;
                }
                if (valueList.Count == 0)
                {
                    Remove(key);
                }
            }
            return n;
        }

        /// <summary>
        /// Gets the total number of values contained in the MultiMap.
        /// </summary>
        public int CountAll
        {
            get
            {
                int n = 0;

                foreach (HashSet<TValue> valueList in Values)
                {
                    n += valueList.Count;
                }
                return n;
            }
        }

        /// <summary>
        /// Determines whether the MultiMap contains an element with a specific
        /// key / value pair.
        /// </summary>
        /// <param name="key">Key of the element to search for.</param>
        /// <param name="value">Value of the element to search for.</param>
        /// <returns>true if the element was found; otherwise false.</returns>
        public bool Contains(TKey key, TValue value)
        {
            HashSet<TValue> valueList;

            if (TryGetValue(key, out valueList))
            {
                return valueList.Contains(value);
            }
            return false;
        }

        /// <summary>
        /// Determines whether the MultiMap contains an element with a specific value.
        /// </summary>
        /// <param name="value">Value of the element to search for.</param>
        /// <returns>true if the element was found; otherwise false.</returns>
        public bool Contains(TValue value)
        {
            foreach (HashSet<TValue> valueList in Values)
            {
                if (valueList.Contains(value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}