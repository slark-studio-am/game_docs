using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public enum BattleUnitType {
    none,
    ally,
    enemy
}
public class BattleUnitCard : MonoBehaviour
{
    [Header("Config")] 
    [SerializeField] private int battleUnitNo = default;
    [SerializeField] private BattleUnitType battleUnitType = default;
    [SerializeField] private Sprite allySprite = default;
    [SerializeField] private Sprite enemySprite = default;
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer = default;
    [Header("UI")] 
    [SerializeField] private TextMeshPro titleText= default;
    [SerializeField] private TextMeshPro ocupationText = default;
    [SerializeField] private TextMeshPro physicalAttackText = default;
    [SerializeField] private TextMeshPro healthPointText = default;
    [SerializeField] private TextMeshPro manaPointText = default;
    [SerializeField] private SpriteRenderer spriteRenderer = default;
    [SerializeField] private Shakeable shakeable = default;
    [SerializeField] private Transform physicalSpeedBar = default;

    [Header("Event SO")] 
    [SerializeField] private IntEventChannelSO onClickEvent = default;

    [SerializeField] private TextUIEventChannelSO textUIEventChannel = default;
    [SerializeField] private TextUIConfigurationSO damageTextUISettings = default;

    [SerializeField] private BuffIconEventChannelSO buffIconEventChannel = default;

    [SerializeField] private RandomSeedSO randomSeed = default;
    [SerializeField] private GameActionSO gameAction = default;
    
    // ---- Action ----
    public UnityAction<BattleUnitCard> OnPhysicalAttackInvoke = default;
    // ----
    private Unit _unit = default;
    public Unit Unit => _unit;

    private float _healthPoint = default;
    private float _healthPointMax = default;
    private float _manaPoint = default;
    private float _manaPointMax = default;
    private float _physicalSpeed = default;
    private float _physicalTimer = default;

    private SortedDictionary<int, BuffIcon> _buffIconTable = default;

    public bool IsSelected {
        get => spriteRenderer.gameObject.activeSelf;
        set => spriteRenderer.gameObject.SetActive(value);
    }

    public int BattleUnitNo {
        get => battleUnitNo;
        set => battleUnitNo = value;
    }

    public BattleUnitType BattleUnitType {
        get => battleUnitType;
        set => ChangeBattleUnitType(value);
    }

    private void Awake()
    {
        _unit = new Unit();
        _buffIconTable = new SortedDictionary<int, BuffIcon>();
        InitActions();
    }

    private void ChangeBattleUnitType(BattleUnitType type)
    {
        if (type == battleUnitType) return;
            
        battleUnitType = type;
        backgroundSpriteRenderer.sprite = battleUnitType switch
        {
            BattleUnitType.ally => allySprite,
            BattleUnitType.enemy => enemySprite,
            _ => backgroundSpriteRenderer.sprite
        };
    }

    public void LinkCard(UnitCardSO unitCard) {
        _unit.LinkCard(this.battleUnitType, unitCard);
    }

    public UnitCardSO GetCard() {
        return _unit.GetCard();
    }

    private void InitActions() {
        _unit.Actions.OnNameChanged += (UnitActionParams p, string a) => {
            if (p.triggerUnit != _unit) return;
            titleText.SetText(a);
            ocupationText.SetText("策划没提供");
        };
        _unit.Actions.OnHpChanged += (UnitActionParams p, float a ) => {
            if (p.triggerUnit != _unit) return;
            _healthPoint = a;
            UpdateHealthPointText();
        };
        _unit.Actions.OnTakenPhysicalDamage += (UnitActionParams p, float a) => {
            if (p.triggerUnit != _unit) return;
            StartCoroutine(FlashText_Coroutine(healthPointText.gameObject));
            
            var pos = this.transform.position;
            var randomPosition = new Vector3(pos.x + Random.Range(-50f,50f), pos.y + Random.Range(-50f,50f),pos.z);
            textUIEventChannel.RaiseRequestEvent(randomPosition, (-Mathf.FloorToInt(a)).ToString(),damageTextUISettings);
        };
        _unit.Actions.OnHpMaxChanged += (UnitActionParams p, float b) => {
            if (p.triggerUnit != _unit) return;    
            _healthPointMax = b;
            UpdateHealthPointText();
        };
        _unit.Actions.OnMpChanged += (UnitActionParams p, float a) => {
            if (p.triggerUnit != _unit) return;
            _manaPoint = a;
            UpdateManaPointText();
        };
        _unit.Actions.OnMpMaxChanged += (UnitActionParams p, float b) => {
            if (p.triggerUnit != _unit) return;
            _manaPointMax = b;
            UpdateManaPointText();
        };
        _unit.Actions.OnSpriteChanged += (UnitActionParams p, Sprite a) => {
            if (p.triggerUnit != _unit) return;
            spriteRenderer.sprite = a;
        };
        _unit.Actions.OnPhysicalPointChanged += (UnitActionParams p, float a, float b) => {
            if (p.triggerUnit != _unit) return;
            physicalAttackText.SetText($"物攻:{a},{b}");
        };
        _unit.Actions.OnPhysicalAttackInvoke += (UnitActionParams p) => {
            if (p.triggerUnit != _unit) return;
            shakeable.Shake(); // 行动动画
            OnPhysicalAttackInvoke?.Invoke(this);
        };
        _unit.Actions.OnPhysicalSpeedChanged += (UnitActionParams p, float a) => {
            if (p.triggerUnit != _unit) return;
            _physicalSpeed = a;
            UpdatePhysicalSpeed();
        };
        _unit.Actions.OnPhysicalSpeedTimer += (UnitActionParams p, float a) => {
            if (p.triggerUnit != _unit) return;
            _physicalTimer = a;
            UpdatePhysicalSpeed();
        };
        _unit.Actions.OnMpInvoke += (UnitActionParams p) => {
            if (p.triggerUnit != _unit) return;
            gameAction.UnitAction.OnMpInvoke?.Invoke(p);
        };
    }

    private void UpdateHealthPointText() {
        healthPointText.SetText($"生命:{Mathf.FloorToInt(_healthPoint)}/{Mathf.FloorToInt(_healthPointMax)}"); 
    }

    private void UpdateManaPointText() {
        manaPointText.SetText($"法力:{Mathf.FloorToInt(_manaPoint)}/{Mathf.FloorToInt(_manaPointMax)}");
    }

    private void UpdatePhysicalSpeed() {
        physicalSpeedBar.localScale = new Vector3(1, Mathf.Min(_physicalTimer, _physicalSpeed)/_physicalSpeed, 1);
    }
    
    private void Update() {
        _unit.Update_Loop();
    }
    private void OnMouseDown() {
        IsSelected = !IsSelected;
        onClickEvent.RaiseEvent(battleUnitNo);
    }

    public void EnterState(UnitState state) {
        _unit.EnterState(state);
    }

    public void PhysicalAttackTarget(BattleUnitCard targetUnitCard) {
        var attackDamage = this._unit.AttackPhysicalDamage(randomSeed.UnitRandom);
        targetUnitCard._unit.TakenPhysicalDamage(randomSeed.UnitRandom, attackDamage, this._unit);
   }

    private static IEnumerator FlashText_Coroutine(GameObject go) {
       for(float i = 0; i < 0.5f; i += 0.1f) {
           go.SetActive(!go.activeSelf);
           yield return new WaitForSeconds(0.1f);
       } 
       go.SetActive(true);
    }

    public bool GetAvailable() {
        return gameObject.activeSelf && _unit.GetAvailable();
    }
    
    public void AddBuff(BuffCardSO card, float layerCount) {
        var buffId = card.CardId;
        if (_buffIconTable.ContainsKey(buffId)) {
            _buffIconTable[buffId].AddLayerCount(layerCount);
        } else {
            var buffIcon = buffIconEventChannel.RaiseRequestEvent(card, _unit, layerCount);
            _buffIconTable.Add(buffId, buffIcon);
            _buffIconTable[buffId].OnLayerCountChanged += OnLayerCountChanged;
        }
        SortAllBuffIcon();
    }

    private void OnLayerCountChanged(BuffIcon triggerBuffIcon) {
        if (triggerBuffIcon.LayerCount > 0) return;
        
        var buffId = triggerBuffIcon.CardSo.CardId;
        Assert.IsTrue(_buffIconTable[buffId] == triggerBuffIcon, "wrong trigger table");
        _buffIconTable[buffId].OnLayerCountChanged -= OnLayerCountChanged;
        _buffIconTable.Remove(buffId);
        
        buffIconEventChannel.RaiseReturnEvent(triggerBuffIcon.gameObject);
    }

    private void SortAllBuffIcon() {
        if (0 == _buffIconTable.Count) return;
        
        var pos = this.transform.position;
        var size = GetComponent<SpriteRenderer>().bounds.size;
        
        pos.x -= size.x / 2f;
        pos.y += size.y / 2f;
        float offsetY = default;
        
        foreach (var kvp in _buffIconTable) {
            size = kvp.Value.GetComponent<SpriteRenderer>().bounds.size;
            pos.x -= size.x / 2f + 2f;
            pos.y -= size.y / 2f + 1f;
            offsetY = size.y * 1.5f;
            break;
        }

        foreach (var v in _buffIconTable.Select(kvp => kvp.Value)) {
            if (v.transform.position != pos) v.transform.position = pos;
            if (!v.gameObject.activeSelf) v.gameObject.SetActive(true);
            pos.y -= offsetY - 2;
        }
    }
}
