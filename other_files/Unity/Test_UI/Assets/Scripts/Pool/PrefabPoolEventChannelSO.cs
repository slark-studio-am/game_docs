using UnityEngine;

[CreateAssetMenu(menuName = "Events/PrefabPool Event Channel")]
public class PrefabPoolEventChannelSO : ScriptableObject {
    public RequestAction OnRequestAction = default;
    public ReturnAction OnReturnAction = default;

    public GameObject RaiseRequestEvent(GameObject prefab) {
        return OnRequestAction?.Invoke(prefab);
    }

    public void RaiseReturnEvent(GameObject prefab, GameObject member) {
        OnReturnAction?.Invoke(prefab, member);
    }
}

public delegate GameObject RequestAction(GameObject prefab);
public delegate void ReturnAction(GameObject prefab, GameObject member);