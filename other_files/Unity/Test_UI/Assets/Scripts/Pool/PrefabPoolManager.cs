using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPoolManager : MonoBehaviour
{
    [SerializeField] private PrefabPoolListSO poolList = default;
    [SerializeField] private PrefabPoolEventChannelSO eventChannel = default;

    private Dictionary<GameObject, PrefabPoolSO> _dictionary = default;

    private void Awake() {
        _dictionary = new Dictionary<GameObject, PrefabPoolSO>();
        RegisterChannel();
    }

    private void Start() {
        foreach (PrefabPoolSO pool in poolList) {
            InitializePool(pool);
        }
    }

    private void OnDestroy() {
        UnregisterChannel();
    }

    private void InitializePool(PrefabPoolSO pool) {
        _dictionary.Add(pool.Prefab, pool);

        var poolRoot = new GameObject("Pool_" + pool.Prefab.name).transform;
        poolRoot.SetParent(this.transform);
        pool.SetPoolRoot(poolRoot);
        pool.Prewarm();
    }

    private void RegisterChannel() {
        eventChannel.OnRequestAction += OnRequestEvent;
        eventChannel.OnReturnAction += OnReturnEvent;
    }

    private void UnregisterChannel() {
        eventChannel.OnRequestAction -= OnRequestEvent;
        eventChannel.OnReturnAction -= OnReturnEvent;
    }

    private GameObject OnRequestEvent(GameObject prefab) {
        return RequestObject(prefab);
    }

    private void OnReturnEvent(GameObject prefab, GameObject member) {
        ReturnObject(prefab, member);
    }

    private GameObject RequestObject(GameObject prefab) {
        return GetPrefabPool(prefab)?.Request();
    }

    private void ReturnObject(GameObject prefab, GameObject member) {
        GetPrefabPool(prefab)?.Return(member);
    }

    private PrefabPoolSO GetPrefabPool(GameObject prefab) {
        return _dictionary.ContainsKey(prefab) ? _dictionary[prefab] : null;
    }
}
