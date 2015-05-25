using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ObservableDictionary<TKey, TValue>
{
    private TKey _OBSERVE_ALL;

    public TKey OBSERVE_ALL
    {
        get { return _OBSERVE_ALL; }
        set { _OBSERVE_ALL = value; }
    }

    private Dictionary<TKey,TValue> _dictionary;
    Dictionary<TKey, List<Observer>> observers;
    public ObservableDictionary()
    {
        _dictionary = new Dictionary<TKey, TValue>();
        observers = new Dictionary<TKey, List<Observer>>();
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
                        o.UpdateObserver(value);
                    }
                    if (_OBSERVE_ALL != null)
                    {
                        foreach (Observer o in observers[_OBSERVE_ALL])
                        {
                            o.UpdateObserver(value);
                        }
                    }
                }
            }
        }
    }

    public void AddObserver(Observer o, TKey jobToObserve)
    {
        List<Observer> list;
        if (observers.TryGetValue(jobToObserve, out list))
        {
            list.Add(o);
        }
        else
        {
            list = new List<Observer>();
            list.Add(o);
            observers.Add(jobToObserve, list);
        }
    }

    public Dictionary<TKey, TValue>.ValueCollection GetValues()
    {
        return _dictionary.Values;
    }


    public Dictionary<TKey,TValue>.KeyCollection GetKeys()
    {
        return _dictionary.Keys;
    }
    
}
