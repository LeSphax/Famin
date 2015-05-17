using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ObservableDictionary<TKey, TValue>
{
    private Dictionary<TKey,TValue> _dictionary;
    Dictionary<TKey, List<Observer>> observers;
    public ObservableDictionary(Dictionary<TKey, List<Observer>> observers)
    {
        _dictionary = new Dictionary<TKey, TValue>();
        this.observers = observers;
    }

    public void Add(TKey key,TValue value){
        _dictionary.Add(key, value);
    }

    public void Remove(TKey key)
    {
        _dictionary.Remove(key);
    }

    public bool TryGetValue(TKey key,out TValue value)
    {
        return _dictionary.TryGetValue(key, out value);
    }

    public TValue this[TKey key]
    {
        get
        {
            return _dictionary[key];
        }
        set
        {
            TValue v;
            if (_dictionary.TryGetValue(key,out v)){
                if (!Equals(v,value))
                {
                    _dictionary[key] = value;
                    foreach (Observer o in observers[key])
                    {
                        o.update(value);
                    }
                }
            }
        }
    }
    
}
