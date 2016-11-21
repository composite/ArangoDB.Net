using System.Text;

namespace System.Collections.Generic
{
    using System.Collections;

    /// <summary>
    /// A map of objects whose mapping entries are sequenced based on the order in which they were
    /// added. This data structure has fast <c>O(1)</c> search time, deletion time, and insertion time
    /// </summary>
    /// <remarks>
    /// This class is not thread safe.
    /// This class is not a really replication of JDK LinkedDictionary{K, V},
    /// this class is an adaptation of SequencedHashMap with generics.
    /// </remarks>
    internal class LinkedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        protected class Entry : IEntry<TKey, TValue>
        {
            private readonly TKey key;

            public Entry(TKey key, TValue value)
            {
                this.key = key;
                Value = value;
            }

            public TKey Key
            {
                get { return key; }
                set { throw new NotSupportedException(); }
            }

            public TValue Value { get; set; }

            public Entry Next { get; set; }

            public Entry Prev { get; set; }

            #region System.Object Members

            public override int GetHashCode()
            {
                return ((key == null ? 0 : key.GetHashCode()) ^ (Value == null ? 0 : Value.GetHashCode()));
            }

            public override bool Equals(object obj)
            {
                Entry other = obj as Entry;
                if (other == null) return false;
                if (other == this) return true;

                return ((key == null ? other.Key == null : key.Equals(other.Key)) &&
                                (Value == null ? other.Value == null : Value.Equals(other.Value)));
            }

            public override string ToString()
            {
                return "[" + key + "=" + Value + "]";
            }

            #endregion System.Object Members
        }

        private readonly Entry header;
        private readonly Dictionary<TKey, Entry> entries;
        private long version;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedDictionary{K,V}"/> class that is empty,
        /// has the default initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        public LinkedDictionary()
            : this(0, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedDictionary{K,V}"/> class that is empty,
        /// has the specified initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="LinkedDictionary{K,V}"/> can contain.</param>
        public LinkedDictionary(int capacity)
            : this(capacity, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedDictionary{K,V}"/> class that is empty, has the default initial capacity, and uses the specified <see cref="IEqualityComparer{K}"/>.
        /// </summary>
        /// <param name="equalityComparer">The <see cref="IEqualityComparer{K}"/> implementation to use when comparing keys, or null to use the default EqualityComparer for the type of the key.</param>
        public LinkedDictionary(IEqualityComparer<TKey> equalityComparer)
            : this(0, equalityComparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedDictionary{K,V}"/> class that is empty, has the specified initial capacity, and uses the specified <see cref="IEqualityComparer{K}"/>.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="LinkedDictionary{K,V}"/> can contain.</param>
        /// <param name="equalityComparer">The <see cref="IEqualityComparer{K}"/> implementation to use when comparing keys, or null to use the default EqualityComparer for the type of the key.</param>
        public LinkedDictionary(int capacity, IEqualityComparer<TKey> equalityComparer)
        {
            header = CreateSentinel();
            entries = new Dictionary<TKey, Entry>(capacity, equalityComparer);
        }

        #region IDictionary<TKey,TValue> Members

        public virtual bool ContainsKey(TKey key)
        {
            return entries.ContainsKey(key);
        }

        public virtual void Add(TKey key, TValue value)
        {
            Entry e = new Entry(key, value);
            entries.Add(key, e);
            version++;
            InsertEntry(e);
        }

        public virtual bool Remove(TKey key)
        {
            return RemoveImpl(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            Entry entry;
            bool result = entries.TryGetValue(key, out entry);
            if (result)
                value = entry.Value;
            else
                value = default(TValue);

            return result;
        }

        public TValue this[TKey key]
        {
            get
            {
                return entries[key].Value;
            }
            set
            {
                Entry e;
                if (entries.TryGetValue(key, out e))
                    OverrideEntry(e, value);
                else
                    Add(key, value);
            }
        }

        private void OverrideEntry(Entry e, TValue value)
        {
            version++;
            RemoveEntry(e);
            e.Value = value;
            InsertEntry(e);
        }

        public virtual ICollection<TKey> Keys
        {
            get { return new KeyCollection(this); }
        }

        public virtual ICollection<TValue> Values
        {
            get { return new ValuesCollection(this); }
        }

        #endregion IDictionary<TKey,TValue> Members

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public virtual void Clear()
        {
            version++;

            entries.Clear();

            header.Next = header;
            header.Prev = header;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return Contains(item.Key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            foreach (KeyValuePair<TKey, TValue> pair in this)
                array.SetValue(pair, arrayIndex++);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public virtual int Count
        {
            get { return entries.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        #endregion ICollection<KeyValuePair<TKey,TValue>> Members

        #region IEnumerable Members

        public virtual IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion IEnumerable Members

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion IEnumerable<KeyValuePair<TKey,TValue>> Members

        #region LinkedDictionary Members

        private bool IsEmpty
        {
            get { return header.Next == header; }
        }

        public virtual bool IsFixedSize
        {
            get { return false; }
        }

        public virtual TKey FirstKey
        {
            get { return (First == null) ? default(TKey) : First.Key; }
        }

        public virtual TValue FirstValue
        {
            get { return (First == null) ? default(TValue) : First.Value; }
        }

        public virtual TKey LastKey
        {
            get { return (Last == null) ? default(TKey) : Last.Key; }
        }

        public virtual TValue LastValue
        {
            get { return (Last == null) ? default(TValue) : Last.Value; }
        }

        public virtual bool Contains(TKey key)
        {
            return ContainsKey(key);
        }

        public virtual bool ContainsValue(TValue value)
        {
            if (value == null)
            {
                for (Entry entry = header.Next; entry != header; entry = entry.Next)
                {
                    if (entry.Value == null) return true;
                }
            }
            else
            {
                for (Entry entry = header.Next; entry != header; entry = entry.Next)
                {
                    if (value.Equals(entry.Value)) return true;
                }
            }
            return false;
        }

        #endregion LinkedDictionary Members

        private static Entry CreateSentinel()
        {
            Entry s = new Entry(default(TKey), default(TValue));
            s.Prev = s;
            s.Next = s;
            return s;
        }

        private static void RemoveEntry(Entry entry)
        {
            entry.Next.Prev = entry.Prev;
            entry.Prev.Next = entry.Next;
        }

        private void InsertEntry(Entry entry)
        {
            entry.Next = header;
            entry.Prev = header.Prev;
            header.Prev.Next = entry;
            header.Prev = entry;
        }

        private Entry First
        {
            get { return (IsEmpty) ? null : header.Next; }
        }

        private Entry Last
        {
            get { return (IsEmpty) ? null : header.Prev; }
        }

        private bool RemoveImpl(TKey key)
        {
            Entry e;
            bool result = false;
            if (entries.TryGetValue(key, out e))
            {
                result = entries.Remove(key);
                version++;
                RemoveEntry(e);
            }
            return result;
        }

        #region System.Object Members

        public override string ToString()
        {
            StringBuilder buf = new StringBuilder();
            buf.Append('[');
            for (Entry pos = header.Next; pos != header; pos = pos.Next)
            {
                buf.Append(pos.Key);
                buf.Append('=');
                buf.Append(pos.Value);
                if (pos.Next != header)
                {
                    buf.Append(',');
                }
            }
            buf.Append(']');

            return buf.ToString();
        }

        #endregion System.Object Members

        private class KeyCollection : ICollection<TKey>
        {
            private readonly LinkedDictionary<TKey, TValue> dictionary;

            public KeyCollection(LinkedDictionary<TKey, TValue> dictionary)
            {
                this.dictionary = dictionary;
            }

            #region ICollection<TKey> Members

            void ICollection<TKey>.Add(TKey item)
            {
                throw new NotSupportedException("LinkedDictionary KeyCollection is readonly.");
            }

            void ICollection<TKey>.Clear()
            {
                throw new NotSupportedException("LinkedDictionary KeyCollection is readonly.");
            }

            bool ICollection<TKey>.Contains(TKey item)
            {
                foreach (TKey key in this)
                {
                    if (key.Equals(item))
                        return true;
                }
                return false;
            }

            public void CopyTo(TKey[] array, int arrayIndex)
            {
                foreach (TKey key in this)
                    array.SetValue(key, arrayIndex++);
            }

            bool ICollection<TKey>.Remove(TKey item)
            {
                throw new NotSupportedException("LinkedDictionary KeyCollection is readonly.");
            }

            public int Count
            {
                get { return dictionary.Count; }
            }

            bool ICollection<TKey>.IsReadOnly
            {
                get { return true; }
            }

            #endregion ICollection<TKey> Members

            #region IEnumerable<TKey> Members

            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            #endregion IEnumerable<TKey> Members

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<TKey>)this).GetEnumerator();
            }

            #endregion IEnumerable Members

            private class Enumerator : ForwardEnumerator<TKey>
            {
                public Enumerator(LinkedDictionary<TKey, TValue> dictionary) : base(dictionary)
                {
                }

                public override TKey Current
                {
                    get
                    {
                        if (dictionary.version != version)
                            throw new InvalidOperationException("Enumerator was modified");

                        return current.Key;
                    }
                }
            }
        }

        private class ValuesCollection : ICollection<TValue>
        {
            private readonly LinkedDictionary<TKey, TValue> dictionary;

            public ValuesCollection(LinkedDictionary<TKey, TValue> dictionary)
            {
                this.dictionary = dictionary;
            }

            #region ICollection<TValue> Members

            void ICollection<TValue>.Add(TValue item)
            {
                throw new NotSupportedException("LinkedDictionary ValuesCollection is readonly.");
            }

            void ICollection<TValue>.Clear()
            {
                throw new NotSupportedException("LinkedDictionary ValuesCollection is readonly.");
            }

            bool ICollection<TValue>.Contains(TValue item)
            {
                foreach (TValue value in this)
                {
                    if (value.Equals(item))
                        return true;
                }
                return false;
            }

            public void CopyTo(TValue[] array, int arrayIndex)
            {
                foreach (TValue value in this)
                    array.SetValue(value, arrayIndex++);
            }

            bool ICollection<TValue>.Remove(TValue item)
            {
                throw new NotSupportedException("LinkedDictionary ValuesCollection is readonly.");
            }

            public int Count
            {
                get { return dictionary.Count; }
            }

            bool ICollection<TValue>.IsReadOnly
            {
                get { return true; }
            }

            #endregion ICollection<TValue> Members

            #region IEnumerable<TKey> Members

            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            {
                return new Enumerator(dictionary);
            }

            #endregion IEnumerable<TKey> Members

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<TValue>)this).GetEnumerator();
            }

            #endregion IEnumerable Members

            private class Enumerator : ForwardEnumerator<TValue>
            {
                public Enumerator(LinkedDictionary<TKey, TValue> dictionary) : base(dictionary)
                {
                }

                public override TValue Current
                {
                    get
                    {
                        if (dictionary.version != version)
                            throw new InvalidOperationException("Enumerator was modified");

                        return current.Value;
                    }
                }
            }
        }

        private abstract class ForwardEnumerator<T> : IEnumerator<T>
        {
            protected readonly LinkedDictionary<TKey, TValue> dictionary;
            protected Entry current;
            protected readonly long version;

            public ForwardEnumerator(LinkedDictionary<TKey, TValue> dictionary)
            {
                this.dictionary = dictionary;
                version = dictionary.version;
                current = dictionary.header;
            }

            #region IDisposable Members

            public void Dispose()
            {
            }

            #endregion IDisposable Members

            #region IEnumerator Members

            public bool MoveNext()
            {
                if (dictionary.version != version)
                    throw new InvalidOperationException("Enumerator was modified");

                if (current.Next == dictionary.header)
                    return false;

                current = current.Next;

                return true;
            }

            public void Reset()
            {
                current = dictionary.header;
            }

            object IEnumerator.Current
            {
                get { return ((IEnumerator<T>)this).Current; }
            }

            #region IEnumerator<T> Members

            public abstract T Current { get; }

            #endregion IEnumerator<T> Members

            #endregion IEnumerator Members
        }

        private class Enumerator : ForwardEnumerator<KeyValuePair<TKey, TValue>>
        {
            public Enumerator(LinkedDictionary<TKey, TValue> dictionary) : base(dictionary)
            {
            }

            public override KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    if (dictionary.version != version)
                        throw new InvalidOperationException("Enumerator was modified");

                    return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                }
            }
        }

        protected abstract class BackwardEnumerator<T> : IEnumerator<T>
        {
            protected readonly LinkedDictionary<TKey, TValue> dictionary;
            private Entry current;
            protected readonly long version;

            public BackwardEnumerator(LinkedDictionary<TKey, TValue> dictionary)
            {
                this.dictionary = dictionary;
                version = dictionary.version;
                current = dictionary.header;
            }

            #region IDisposable Members

            public void Dispose()
            {
            }

            #endregion IDisposable Members

            #region IEnumerator Members

            public bool MoveNext()
            {
                if (dictionary.version != version)
                    throw new InvalidOperationException("Enumerator was modified");

                if (current.Prev == dictionary.header)
                    return false;

                current = current.Prev;

                return true;
            }

            public void Reset()
            {
                current = dictionary.header;
            }

            object IEnumerator.Current
            {
                get { return ((IEnumerator<T>)this).Current; }
            }

            #region IEnumerator<T> Members

            public abstract T Current { get; }

            #endregion IEnumerator<T> Members

            #endregion IEnumerator Members
        }
    }
}