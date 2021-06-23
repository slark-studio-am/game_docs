using System;
using UnityEngine;

public enum UnitRarity { Basic, Special, Common, Uncommon, Rare, Curse };

public enum TargetPrefix { Ally, Enemy, All }

public enum TargetPosition { Random, Single, FirstRow, LastRow, Col, }

[Serializable]
public class TargetInfo {
    public TargetPrefix prefix;
    public TargetPosition position;
}

public enum UnitType {
    None,
    Hero,
    Summon
}

[CreateAssetMenu(fileName = "NewUnitCard", menuName = "Card/Unit Card")]
public class UnitCardSO : BaseCardSO {
    [SerializeField] private int unitId = default;
    [Tooltip("单位类型")] [SerializeField] private UnitType unitType = default;
    [Tooltip("卡牌稀有度")] [SerializeField] private UnitRarity rarity = default; 
    [Tooltip("卡牌等级")] [SerializeField] private int level = 0;
    [Tooltip("生命值最大值")] [SerializeField] private float healthPoint = default;
    [Tooltip("生命值每级加成")] [SerializeField] private float healthPointEachLevel = default;
    [Tooltip("物攻")] [SerializeField] private float physicalPointMin = default;
    [Tooltip("物攻")] [SerializeField] private float physicalPointMax = default;
    [Tooltip("物攻每级加成")] [SerializeField] private float physicalPointEachLevel = default;
    [Tooltip("法强")] [SerializeField] private float magicPoint = default;
    [Tooltip("法强每级加成")] [SerializeField] private float magicPointEachLevel = default;
    [Range(-100f,100f)][Tooltip("物伤减免")] [SerializeField] private float physicalReduceRate = default;
    [Range(-100f,100f)][Tooltip("法伤减免")] [SerializeField] private float magicReduceRate = default;
    [Tooltip("暴击率")] [SerializeField] private float criticalStrikeRate = default;
    [Tooltip("暴击效果")] [SerializeField] private float criticalStrikeMultiplier = default;
    [Tooltip("闪避率")] [SerializeField] private float evasionRate = default;
    [Tooltip("攻速")] [SerializeField] private float physicalSpeed = default;
    [Tooltip("魔法最大值")] [SerializeField] private float manaPoint = default; 
    [Tooltip("攻击回蓝")] [SerializeField] private float physicalAttackRestoreMana = default;
    [Tooltip("吸血率")] [SerializeField] private float suckBloodRate = default;
    [Tooltip("护盾值")] [SerializeField] private float shieldPoint = default;
    [Tooltip("承伤回蓝")] [SerializeField] private float underAttackRestoreMana = default;
    [Tooltip("普攻目标")] [SerializeField] private TargetInfo physicalTarget = default;
    [Tooltip("主动技能")] [SerializeField] private AbilitySO[] activeAbilityList = default;
    [Tooltip("被动技能")] [SerializeField] private AbilitySO[] passiveAbilityList = default;
    // ----
    public float HealthPointMax => healthPoint ;
    public float ManaPointMax => manaPoint;
    public float PhysicalPointMin => physicalPointMin;
    public float PhysicalPointMax => physicalPointMax;
    public float PhysicalSpeed => physicalSpeed;
    public float CriticalStrikeRate => criticalStrikeRate;
    public float CriticalStrikeMultiplier => criticalStrikeMultiplier;
    public float EvasionRate => evasionRate;
    public float ShieldPoint => shieldPoint;
    public float PhysicalReduceRate => physicalReduceRate;
    public float PhysicalAttackRestoreMana => physicalAttackRestoreMana;
    public float UnderAttackRestoreMana => underAttackRestoreMana;
    public float SuckBloodRate => suckBloodRate;
}
