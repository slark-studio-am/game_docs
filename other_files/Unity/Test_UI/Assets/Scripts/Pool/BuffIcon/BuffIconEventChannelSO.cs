using UnityEngine;

[CreateAssetMenu(menuName = "Events/BuffIcon Event Channel")]
public class BuffIconEventChannelSO : ScriptableObject {
    [SerializeField] private PrefabPoolSO pool = default;

    public BuffIcon RaiseRequestEvent(BuffCardSO card, Unit targetUnit, float layerCount) {
        var x = pool.Request();
        var buffIcon = x.GetComponent<BuffIcon>();
        buffIcon.Show(card, targetUnit, layerCount);
        return buffIcon;
    }

    public void RaiseReturnEvent(GameObject member) {
        pool.Return(member);
    }

    public bool RaiseContainsEvent(GameObject member) {
        return pool.Contains(member);
    }
}
