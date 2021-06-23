using UnityEngine;
using UnityEngine.Localization;

public enum AbilityType {
    active,
    passive
}

[CreateAssetMenu(fileName = "NewAbility", menuName = "Ability/Ability")]
public class AbilitySO : ScriptableObject 
{
    [SerializeField] private int _id = default;
    [SerializeField] private string _name = default;
    [SerializeField] private LocalizedString _localizedName = default;
    [SerializeField] private string _description = default;
    [SerializeField] private LocalizedString _localizedDescription = default;
    [SerializeField] private AbilityType _type = default;
    [SerializeField] private float _manaCost = default;
    // [SerializeField] private 

    public void DoAction(Unit selfUnit) { 
        switch (_id)
        {
            case 1:
                {
                    // selfUnit.ChangeArmorPoint(50);
                }
                break;
            default:
                break;
        }
    }

    public float ManaCost => _manaCost;
}
