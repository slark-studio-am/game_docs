using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UnitActionParams {
    public Unit triggerUnit = default;
}

[Serializable]
public class UnitAction {
    public UnityAction<UnitActionParams, string> OnNameChanged = default;
    public UnityAction<UnitActionParams, float> OnHpChanged = default; // 当前血量变动
    public UnityAction<UnitActionParams, float> OnHpMaxChanged = default; // 当前血量最大值变动
    public UnityAction<UnitActionParams, float> OnMpChanged = default;
    public UnityAction<UnitActionParams, float> OnMpMaxChanged = default;
    public UnityAction<UnitActionParams> OnMpInvoke = default;
    public UnityAction<UnitActionParams, float, float> OnPhysicalPointChanged = default;
    public UnityAction<UnitActionParams, float> OnPhysicalSpeedChanged = default;
    public UnityAction<UnitActionParams, float> OnPhysicalSpeedTimer = default;
    public UnityAction<UnitActionParams> OnPhysicalAttackInvoke = default;
    public UnityAction<UnitActionParams, Sprite> OnSpriteChanged = default;
    public UnityAction<UnitActionParams, float> OnTakenPhysicalDamage = default;
}