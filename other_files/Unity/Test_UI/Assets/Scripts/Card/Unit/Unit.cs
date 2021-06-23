using System;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

public enum UnitState {
    None,
    Init,
    Pause,
    Idle,
    Death,
    PhysicalAttack,
    MagicAttack
}

public class Unit {
    private UnitCardSO _unitCard;
    private string Name => _unitCard.CardName;
    private float _healthPoint = default;
    private readonly GameFloat _healthPointMax = default;
    private float _manaPoint = default;
    private readonly GameFloat _manaPointMax = default;
    private readonly GameFloat _physicalPoint = default;
    private float _physicalPointMin = default;
    private float _physicalPointMax = default;
    private float _shieldPoint = default;
    private readonly GameFloat _physicalSpeed = default;
    private Sprite _sprite = default;
    private readonly GameFloat _criticalStrikeChance = default;
    private readonly GameFloat _criticalStrikeMultiplier = default;
    private readonly GameFloat _evasionRate = default;
    private readonly GameFloat _physicalReduceRate = default;
    private readonly GameFloat _physicalAttackRestoreMana = default;
    private readonly GameFloat _underAttackRestoreMana = default;
    private readonly GameFloat _suckBloodRate = default;

    public BattleUnitType BattleUnitType = default;
    // ----
    private float _physicalSpeedTimer = default;
    public UnitAction Actions { get; private set; } = default;

    private UnitState _state = default;

    public Unit() {
        _healthPointMax = new GameFloat();
        _manaPointMax = new GameFloat();
        _physicalPoint = new GameFloat();
        _physicalSpeed = new GameFloat();
        _criticalStrikeChance = new GameFloat();
        _criticalStrikeMultiplier = new GameFloat();
        _evasionRate = new GameFloat();
        _physicalReduceRate = new GameFloat();
        _physicalAttackRestoreMana = new GameFloat();
        _underAttackRestoreMana = new GameFloat();
        _suckBloodRate = new GameFloat();

        Actions = new UnitAction();
    }
    
    private void InitActions() {
    }

    public void LinkCard(BattleUnitType unitType, UnitCardSO unitCard) {
        BattleUnitType = unitType;
        EnterState(UnitState.Init);

        ChangeName(unitCard.CardName);

        ChangeHealthPointMax(unitCard.HealthPointMax);
        ChangeHealthPoint(unitCard.HealthPointMax);

        ChangeManaPointMax(unitCard.ManaPointMax);
        ChangeManaPoint(0f);

        ChangePhysicalPoint(unitCard.PhysicalPointMin, unitCard.PhysicalPointMax);
        ChangePhysicalSpeed(unitCard.PhysicalSpeed);

        ChangeSprite(unitCard.PreviewImage);

        ChangeCriticalStrikeRate(unitCard.CriticalStrikeRate);
        ChangeCriticalStrikeMultiplier(unitCard.CriticalStrikeMultiplier);
        ChangeEvasionRate(unitCard.EvasionRate);
        ChangeShieldPoint(unitCard.ShieldPoint);
        ChangePhysicalReduceRate(unitCard.PhysicalReduceRate);
        ChangePhysicalAttackRestoreMana(unitCard.PhysicalAttackRestoreMana);
        ChangeUnderAttackRestoreMana(unitCard.UnderAttackRestoreMana);
        ChangeSuckBloodRate(unitCard.SuckBloodRate);
            
        // 一定要在最后面在赋值
        _unitCard = unitCard;
        EnterState(UnitState.Pause);
    }

    private void ChangeSuckBloodRate(float a) {
        if (!a.Equals(_suckBloodRate.basePoint)) {
            _suckBloodRate.basePoint = a;
        }
    }

    private void ChangeUnderAttackRestoreMana(float a) {
        if (!a.Equals(_underAttackRestoreMana.basePoint)) {
            _underAttackRestoreMana.basePoint = a;
        }
    }

    private void ChangePhysicalAttackRestoreMana(float a) {
        if (!a.Equals(_physicalAttackRestoreMana.basePoint)) {
            _physicalAttackRestoreMana.basePoint = a;
        }
    }

    private void ChangePhysicalReduceRate(float a) {
        if (!a.Equals(_physicalReduceRate.basePoint)) {
            _physicalReduceRate.basePoint = a;
            // TODO action
        }
    }

    private void ChangeShieldPoint(float a) {
        if (!a.Equals(_shieldPoint)) {
            _shieldPoint = a;
            // TODO action
        }
    }

    private void ChangeEvasionRate(float a) {
        if (!a.Equals(_evasionRate.basePoint)) {
            _evasionRate.basePoint = a;
            // TODO action
        }
    }

    private void ChangeCriticalStrikeMultiplier(float a) {
        if (!a.Equals(_criticalStrikeMultiplier.basePoint)) {
            _criticalStrikeMultiplier.basePoint = a;
            // TODO action
        }
    }

    private void ChangeCriticalStrikeRate(float a) {
        if (!a.Equals(_criticalStrikeChance.basePoint)) {
            _criticalStrikeChance.basePoint = a;
            // TODO action
        }
    }

    public UnitCardSO GetCard() {
        return _unitCard;
    }

    private void ChangeName(string n) {
        var p = new UnitActionParams {triggerUnit = this};
        Actions.OnNameChanged?.Invoke(p, n);
    }

    private void ChangeHealthPoint(float a) {
        if (a.Equals(_healthPoint)) return;
        _healthPoint = Mathf.Min(a, (float)_healthPointMax);
        
        var p = new UnitActionParams {triggerUnit = this};
        Actions.OnHpChanged?.Invoke(p, _healthPoint);
    }

    private void ChangeHealthPointMax(float a) {
        if (a.Equals(_healthPointMax.basePoint)) return;
        _healthPointMax.basePoint = a;
        
        var p = new UnitActionParams {triggerUnit = this};
        Actions.OnHpMaxChanged?.Invoke(p, _healthPointMax.basePoint);
    }

    private void ChangeManaPoint(float a) {
        if (a.Equals(_manaPoint)) return;

        var invokeCount = 0;
        _manaPoint = a;
        var manaPointMax = (float) _manaPointMax;
        if (_manaPoint >= manaPointMax) {
            invokeCount = (int)(_manaPoint / manaPointMax);
            _manaPoint -= invokeCount * manaPointMax;
        }
            
        var p = new UnitActionParams {triggerUnit = this};
        Actions.OnMpChanged?.Invoke(p, _manaPoint);

        for (var i = 0; i < invokeCount; ++i) {
            MpInvoke();
        }
    }

    private void MpInvoke() {
        // TODO 绑定主动技能
        var p = new UnitActionParams {triggerUnit = this};
        Actions.OnMpInvoke?.Invoke(p);
    }

    private void ChangeManaPointMax(float a) {
        if (a.Equals(_manaPointMax)) return;
        _manaPointMax.basePoint = a;
        
        var p = new UnitActionParams {triggerUnit = this};
        Actions.OnMpMaxChanged?.Invoke(p, a);
    }

    private void ChangeSprite(Sprite a) {
        if (a == _sprite) return;
        _sprite = a;
        var p = new UnitActionParams {triggerUnit = this};
        Actions.OnSpriteChanged?.Invoke(p, a);
    }

    private void ChangePhysicalPoint(float a, float b) {
        if (a.Equals(_physicalPointMin) && b.Equals(_physicalPointMax)) return;
        _physicalPointMin = a;
        _physicalPointMax = b; 
        
        var p = new UnitActionParams {triggerUnit = this};
        Actions.OnPhysicalPointChanged?.Invoke(p, a, b);
    }

    private void ChangePhysicalSpeed(float a) {
        if (a.Equals(_physicalSpeed.basePoint)) return;
        _physicalSpeed.basePoint = a;
        
        var p = new UnitActionParams {triggerUnit = this};
        Actions.OnPhysicalSpeedChanged?.Invoke(p, a);
    }
    
    public bool IsNormalState() {
        return UnitState.None != _state && UnitState.Idle != _state;
    }

    public bool IsInitialized() {
        return UnitState.None != _state;
    }

    /*
    *   1.12 状态机
        状态	攻速条	技能条	说明
        待机	可动	可动	-
        普通攻击	不可动	可动	执行普通攻击的画面效果和对目标造成伤害
        发动主动技能	不可动	可动	执行主动技能的画面效果和相应的技能效果
        暂停	不可动	可动	无法蓄力，无法普通攻击，无法发动主动技能
        其他状态	-	-	除了切换到的新状态外的其他几个状态
        当前状态	切换条件	新状态
        待机	普通攻击进度条蓄满	普通攻击
        待机	法力值蓄满	发动主动技能
        其他状态	受BUFF影响如冻结	暂停
        其他状态	有其他单位处于【普通攻击】或【发动技能】状态	暂停
        普通攻击	普攻动画结束（普通攻击进度条清零）	待机
        发动技能	主动技能动画结束（法力值清零）	待机
        暂停	造成暂停的BUFF时间结束	待机
        当待机状态下同时有多个单位即将切换为【普通攻击】或【发动技能】状态时，使用【单位种子】对待切换的单位进行优先切换顺序的排序。 
     */
    public void EnterState(UnitState newState) {
        switch (_state) {
            case UnitState.Death:
                DeathEnterState(newState);
                break;
            default:
                _state = newState;
                break;
        }
    }

    private void DeathEnterState(UnitState newState) {
        switch (newState) {
            case UnitState.None:
            case UnitState.Init:
                _state = newState;
                break;
            default:
                break;
        }
    }

    public void Update_Loop() {
        if (UnitState.Idle == _state) {
            LoopPhysicalAttack();
        }
    }
    
    private void LoopPhysicalAttack() {
        if (Time.deltaTime > 0f) {
            _physicalSpeedTimer += Time.deltaTime;
            
            var p = new UnitActionParams {triggerUnit = this};
            Actions.OnPhysicalSpeedTimer?.Invoke(p, _physicalSpeedTimer);
        }

        if (_physicalSpeedTimer >= _physicalSpeed)  {
            _physicalSpeedTimer -= _physicalSpeed;
            
            var p = new UnitActionParams {triggerUnit = this};
            Actions.OnPhysicalAttackInvoke?.Invoke(p);
        }
    }

    private bool AutoDoActiveSkills() {
        // TODO 
        /*
        foreach(SkillSO item in _card.ActiveSkills) {
            if (item.ManaCost <= _manaPoint) {
                ChangeManaPoint(-item.ManaCost);
                item.DoAction(this);
                return true;
            }
        }
        */
        return false;
    }

    public float AttackPhysicalDamage(GameRandom random) {
        _physicalPoint.basePoint = random.Range(_physicalPointMin, _physicalPointMax);
        float physicalDamage = _physicalPoint;

        var chance = random.Range(0f, 100f);
        if (chance < _criticalStrikeChance) {
            physicalDamage *= 1f + _criticalStrikeMultiplier/100f;
        }
        
        ChangeManaPoint(_manaPoint + (float)_physicalAttackRestoreMana);
        return physicalDamage;
    }

    public void AddManaPoint(float point) {
        ChangeManaPoint(_manaPoint+point);
    }

    public void AddSuckBloodRate(float a) {
        _suckBloodRate.appendPoint += a;
    }

    public void AddEvasionRate(float a) {
        _evasionRate.appendPoint += a;
    }
    
    public void TakenPhysicalDamage(GameRandom random, float attackPoint, Unit attackUnit) {
        var chance = random.Range(0f, 100f);
        if (chance < _evasionRate) {
            return;
        }

        if (_shieldPoint > 0f) {
            var offset = Mathf.Min(_shieldPoint, attackPoint, 0);
            if (offset > 0) {
                _shieldPoint -= offset;
                attackPoint -= offset;
                // TODO add action shield point change
            }
        }

        attackPoint *= 1f - _physicalReduceRate.Percent();
        ChangeHealthPoint(_healthPoint - attackPoint);

        var p = new UnitActionParams {triggerUnit = this};
        Actions.OnTakenPhysicalDamage?.Invoke(p, attackPoint);
        
        
        if (_healthPoint <= 0f) {
            EnterState(UnitState.Death);
        }
        
        attackUnit.SuckBlood(attackPoint);
        ChangeManaPoint(_manaPoint + Mathf.Min(50f, attackPoint * _underAttackRestoreMana.Percent()));
    }

    private void SuckBlood(float attackPoint) {
        ChangeHealthPoint(_healthPoint + attackPoint * _suckBloodRate.Percent());
    }
    public bool GetAvailable()
    {
        return _state switch
        {
            UnitState.None => false,
            UnitState.Death => false,
            _ => true
        };
    }
}
