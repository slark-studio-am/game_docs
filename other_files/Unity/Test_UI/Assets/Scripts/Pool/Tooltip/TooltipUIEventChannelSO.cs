using UnityEngine;

[CreateAssetMenu(menuName = "Events/TooltipUI Event Channel")]
public class TooltipUIEventChannelSO : ScriptableObject {
    [SerializeField] private PrefabPoolSO pool = default;

    public GameObject RaiseRequestEvent(Vector3 position,Vector2 follow, string caption, string message) {
        var x = pool.Request();
        x.GetComponent<TooltipUI>().Show(position, follow, caption, message);
        return x;
    }

    public void RaiseReturnEvent(GameObject member) {
        pool.Return(member);
    }
}
