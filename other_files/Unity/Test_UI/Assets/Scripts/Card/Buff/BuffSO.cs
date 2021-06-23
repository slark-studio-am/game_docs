using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffBase {
   protected readonly BuffSO So = default;
   public float LayerCount = default;
   public UnityAction OnLayerCountChanged = default;
   
   protected BuffBase(BuffSO so) { So = so; }
   public virtual void Register(Unit targetUnit, float layerCount) {
       throw new NotImplementedException();
   }

   public virtual void Unregister() {
       throw new NotImplementedException();
   }

   public virtual void AddLayerCount(float layerCount) {
       if (layerCount == 0f) return;
       LayerCount += layerCount;
       OnLayerCountChanged?.Invoke();
   }
}

public class Buff001 : BuffBase {
    public Buff001(BuffSO so) : base(so) { }
    
    public override void Register(Unit targetUnit, float layerCount) {
    }
    
    public override void Unregister() {
    }
    
    private void DoAction() {
    }
}

public class Buff002 : BuffBase {
    private const float Rate = 10f;
    private Unit _targetUnit = default;

    public Buff002(BuffSO so) : base(so) { }
    
    public override void Register(Unit targetUnit, float layerCount) {
        _targetUnit = targetUnit;
        LayerCount = layerCount;
        
        So.gameAction.OnBattleUpdateAction += DoAction;
        _targetUnit.AddSuckBloodRate(Rate);
    }
    
    public override void Unregister() {
        _targetUnit.AddSuckBloodRate(-Rate);
        So.gameAction.OnBattleUpdateAction -= DoAction;
        LayerCount = 0;
    }

    private void DoAction() {
        AddLayerCount(-Time.deltaTime);
    }
}

public class BuffSO : ScriptableObject {
    public GameActionSO gameAction = default;
    public BattleSO battleSo = default;
    // ----
    public BuffBase Register(BuffCardSO card, Unit targetUnit, float layerCount) {
       BuffBase buffBase = card.CardId switch {
           1 => new Buff001(this),
           2 => new Buff002(this),
           _ => null
       };
       buffBase?.Register(targetUnit, layerCount);
       return buffBase;
    }

    public void Unregister(BuffBase buffBase) {
        buffBase.Unregister();
    }
}
