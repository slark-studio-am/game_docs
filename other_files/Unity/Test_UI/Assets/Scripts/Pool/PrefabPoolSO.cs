using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pool/Prefab Pool")]
public class PrefabPoolSO : ScriptableObject {
    public GameObject Prefab => prefab;
    [SerializeField] private GameObject prefab = default;
    [SerializeField] private int prewarmNumber = default;

    private  Transform _poolRoot = default;
    private readonly Queue<GameObject> _available = new Queue<GameObject>();

    public void Prewarm() {
        var n = prewarmNumber - _available.Count;
        for (var i = 0; i < n; ++i) {
            _available.Enqueue(Create());
        }
    }

    public GameObject Request() {
        return _available.Count > 0 ? _available.Dequeue() : Create();
    }

    public void Return(GameObject member) {
        if (!member) return;
        member.SetActive(false);
        if (_poolRoot != member.transform.parent)
            member.transform.SetParent(_poolRoot);
        _available.Enqueue(member);
    }

    private GameObject Create() {
        var x = Instantiate(prefab, _poolRoot);
        x.SetActive(false);
        return x;
    }

    private void Clear() {
        _available.Clear();
    }

    public void SetPoolRoot(Transform t) {
        _poolRoot = t;
    }

    public bool Contains(GameObject member) {
        return _available.Contains(member);
    }
}
