using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/TextUI Event Channel")]
public class TextUIEventChannelSO : ScriptableObject{
    [SerializeField] private PrefabPoolSO pool = default;
    
    public void RaiseRequestEvent(Vector3 position, string text, TextUIConfigurationSO settings) {
        var x = pool.Request();
        x.GetComponent<TextUI>().Show(position, text, settings);
    }

    public void RaiseReturnEvent(GameObject member) {
        pool.Return(member);
    }
}