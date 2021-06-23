using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;


public abstract class ArtifactBase {
    protected readonly ArtifactSO So = default;
    protected ArtifactBase(ArtifactSO so) { So = so; }

    public virtual void Register(ArtifactCardSO card) {
        throw new NotImplementedException();
    }

    public virtual void Unregister() {
        throw new NotImplementedException();
    }
}

public class Artifact001 : ArtifactBase {
    public Artifact001(ArtifactSO so) : base(so) { }
    
    public override void Register(ArtifactCardSO card) {
        So.gameAction.UnitAction.OnMpInvoke += DoAction;
    }
    
    public override void Unregister() {
        So.gameAction.UnitAction.OnMpInvoke -= DoAction;
    }

    private void DoAction(UnitActionParams p) {
        if (BattleUnitType.ally != p.triggerUnit.BattleUnitType) return;
        foreach (var x in So.battleSo.AllyBattleUnitCards.Where(x => x.GetAvailable())) {
            x.Unit.AddManaPoint(5f);
        }
    }
}

public class Artifact002 : ArtifactBase {
    private float _triggerTime = default;
    private const float TriggerTimeOffset = 3f;
    private const float BuffLayer = 2f;
    private BuffCardSO _buffCard = default;
    public Artifact002(ArtifactSO so) : base(so) { }

    public override void Register(ArtifactCardSO card) {
        if (null == _buffCard && card.LinkBuffCards.Length > 0) {
            _buffCard = card.LinkBuffCards[0];
        }
        
        _triggerTime = So.battleSo.BattleTimer + TriggerTimeOffset;
        So.gameAction.OnBattleUpdateAction += DoAction;
    }
    
    public override void Unregister() {
        So.gameAction.OnBattleUpdateAction -= DoAction;
    }
    private void DoAction() {
        while (So.battleSo.BattleTimer >=  _triggerTime) {
            _triggerTime += TriggerTimeOffset;
            foreach (var x in So.battleSo.AllyBattleUnitCards.Where(x => x.GetAvailable())) {
                x.AddBuff(_buffCard, BuffLayer);
            } 
        }
    }
}

public class Artifact003 : ArtifactBase {
    public Artifact003(ArtifactSO so) : base(so) { }

    public override void Register(ArtifactCardSO card) {
    }
    
    public override void Unregister() {
    }
}

public class Artifact004 : ArtifactBase {
    private float _triggerTime = default;
    private const float TriggerTimeOffset = 1f;    
    private const float EvasionRate = 1f;
    public Artifact004(ArtifactSO so) : base(so) { }

    public override void Register(ArtifactCardSO card) {
        _triggerTime = So.battleSo.BattleTimer + TriggerTimeOffset;
        So.gameAction.OnBattleUpdateAction += DoAction;
    }
    
    public override void Unregister() {
        So.gameAction.OnBattleUpdateAction -= DoAction;
    }
    
    private void DoAction() {
        while (So.battleSo.BattleTimer >=  _triggerTime) {
            _triggerTime += TriggerTimeOffset;
            foreach (var x in So.battleSo.AllyBattleUnitCards.Where(x => x.GetAvailable())) {
                x.Unit.AddEvasionRate(EvasionRate);
            } 
        }
    }
}

public class ArtifactSO : ScriptableObject {
    public GameActionSO gameAction = default;
    public BattleSO battleSo = default;
    // ----
    private readonly Dictionary<int, ArtifactBase> _artifactTable = default;

    public ArtifactSO() {
        _artifactTable = new Dictionary<int, ArtifactBase> {
            {1, new Artifact001(this)}, 
            {2, new Artifact002(this)},
            {3, new Artifact003(this)},
            {4, new Artifact004(this)}
        };
    }

    public void Register(ArtifactCardSO card) {
        if (!_artifactTable.ContainsKey(card.CardId)) return;
        _artifactTable[card.CardId].Register(card);
    }

    public void Unregister(ArtifactCardSO card) {
        if (!_artifactTable.ContainsKey(card.CardId)) return;
        _artifactTable[card.CardId].Unregister();
    }
}
