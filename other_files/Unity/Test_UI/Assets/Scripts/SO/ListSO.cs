using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSO<T> : ScriptableObject,IEnumerable
{
    [SerializeField] private List<T> _list;

    public int Count {
        get => _list.Count;
        private set {}
    }
    public int Length {
        get => this.Count;
        private set {}
    }

    public T this[int index] {
        get => _list[index];
        private set => _list[index] = value;
    }

    public IEnumerator GetEnumerator() {
        for (var i = 0; i < this.Count; i++)
            yield return this[i];
    }

    public void Clear () {
        _list.Clear();
        _list.TrimExcess();
    }

    public void Add (T item) {
        _list.Add(item);
    }

    public List<T> GetShuffleList(System.Random random, int count, bool repeat = false) {
        var l = new List<T>();
        var oldCount = _list.Count;

        if (repeat || count > oldCount) {
            for (var i = 0; i < count; i++)
                l.Add(_list[random.Next(_list.Count)]);
        } else {
            var n = oldCount;
            var d = new Dictionary<int, T>();
            while (n > 1 && ((oldCount - n) < count)) {
               var k = random.Next(n--);
               if (!d.ContainsKey(n)) d.Add(n, _list[n]);
               if (!d.ContainsKey(k)) d.Add(k, _list[k]);

               var value = d[k];
               d[k] = d[n];
               d[n] = value;
            }

            n = oldCount;
            while ((oldCount - n) < count) 
                l.Add(d[--n]);
        }
        return l;
    }
}
