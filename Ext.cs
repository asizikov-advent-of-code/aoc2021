public static class Ext
{
    public static void PutIfAbsent<TKey, TVal>(this Dictionary<TKey, TVal> dict, TKey key, TVal val)
    {
        if(!dict.ContainsKey(key)) dict.Add(key, val);
    }
}