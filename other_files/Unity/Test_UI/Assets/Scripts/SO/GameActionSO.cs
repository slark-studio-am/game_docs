using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "NewGameAction", menuName = "Events/Game Action")]
public class GameActionSO : ScriptableObject {
    [TextArea] [SerializeField] private string description;
    
    // 单位行动事件
    [SerializeField] private UnitAction unitAction = default;
    // ----
    // 战斗中刷新事件
    public UnityAction OnBattleUpdateAction = default;
    public UnitAction UnitAction => unitAction;

    private void Awake() {
        unitAction = new UnitAction();
        
    }
}
