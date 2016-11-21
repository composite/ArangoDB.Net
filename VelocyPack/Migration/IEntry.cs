namespace System.Collections.Generic
{
    public interface IEntry<TKey, TValue>
    {
        TKey Key { get; set; }
        TValue Value { get; set; }
    }
}
