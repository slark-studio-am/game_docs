using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BattleStatus {
    None,
    Start,
    Idle,
    Pasue,
    End
}
public class BattleGM : MonoBehaviour {
    [Header("UI_Battle")]
    [SerializeField] private BattleUnitCard[] enemyBattleUnitCards = default;
    [SerializeField] private VacancyCard[] enemyVacancyCards = default;
    [SerializeField] private BattleUnitCard[] allyBattleUnitCards = default;
    [SerializeField] private VacancyCard[] allyVacancyCards = default;
    [SerializeField] private TextMeshProUGUI battleButtonText = default;

    [Header("UI_Left")]
    [SerializeField] private CardUI leftCardUI = default;

    [Header("UI_Top")] 
    [SerializeField] private Transform topArtifactIconRoot = default;

    [Header("UI_Bottom")] 
    [SerializeField] private Transform bottomHandCardRoot = default;

    [Header("SO")] 
    [SerializeField] private RandomSeedSO randomSeed = default;
    [SerializeField] private UnitCardListSO enemyBattleUnitUnitCardList = default;
    [SerializeField] private UnitCardListSO allyBattleUnitUnitCardList = default;
    [SerializeField] private ArtifactCardListSO usedArtifactCardList = default;
    [SerializeField] private ArtifactIconUIEventChannelSO artifactIconUIEventChannel = default;
    [SerializeField] private GameActionSO gameAction = default;
    [SerializeField] private BattleSO battle = default;
    [SerializeField] private ArtifactSO artifactSo = default;
    [SerializeField] private CardUIEventChannelSO cardUIEventChannel = default;

    [SerializeField] private BaseCardSO testCard = default;

    private float _battleTimer = default;
    private BattleStatus _battleStatus = default;

    private void Start() {
        foreach (var battleUnitCard in enemyBattleUnitCards) {
            battleUnitCard.OnPhysicalAttackInvoke += OnUnitPhysicalAttackInvoke;
        }

        foreach (var battleUnitCard in allyBattleUnitCards) {
            battleUnitCard.OnPhysicalAttackInvoke += OnUnitPhysicalAttackInvoke;
        }
        
        Invoke("InitBattleUnitCards", 0.5f);
        battle.SetBattleUnitCards(enemyBattleUnitCards, allyBattleUnitCards);
        InitArtifactIconUI();
    }


    private void InitArtifactIconUI() {
        foreach (ArtifactCardSO card in usedArtifactCardList) {
            artifactIconUIEventChannel.RaiseRequestEvent(topArtifactIconRoot, card);
        }
    }

    private void OnUnitPhysicalAttackInvoke(BattleUnitCard unitCard) {
        PauseBattle();
        var targetCard = GetTargetUnitCard(unitCard.BattleUnitType);
        if (targetCard) {
            unitCard.PhysicalAttackTarget(targetCard);
        }
        // TODO 判断是否游戏结束
        StartCoroutine(IdleAllCard_Coroutine());
    }

    private IEnumerator IdleAllCard_Coroutine() {
        yield return new WaitForSeconds(0.5f);
        IdleBattle();
    }

    private BattleUnitCard GetTargetUnitCard(BattleUnitType battleUnitType) {
        return battleUnitType switch
        {
            BattleUnitType.enemy => GetTargetUnitCardImpl(allyBattleUnitCards),
            BattleUnitType.ally => GetTargetUnitCardImpl(enemyBattleUnitCards),
            _ => null
        };
    }

    private BattleUnitCard GetTargetUnitCardImpl(IReadOnlyList<BattleUnitCard> unitCards) {
        var l = new List<int>();
        for (var i = 0; i < unitCards.Count; i++) {
            if (unitCards[i].GetAvailable())
                l.Add(i);
        }

        // TODO 无目标的处理
        return  unitCards[l[randomSeed.UnitRandom.Next(l.Count)]];
    }

    private void ControlAllBattleUnitCard(UnitState state) {
        foreach (var battleUnitCard in enemyBattleUnitCards) {
            if (battleUnitCard.GetAvailable()) {
                battleUnitCard.EnterState(state);
            }
        }

        foreach (var battleUnitCard in allyBattleUnitCards) {
            if (battleUnitCard.GetAvailable()) {
                battleUnitCard.EnterState(state);
            }
        }
    }

    public void OnClickStartBattle() {
        if (BattleStatus.None == _battleStatus ||
            BattleStatus.End == _battleStatus) {
            StartBattle();
        } else {
            IdleBattle();
        }
    }

    private void ChangeBattleTimer(float a) {
        _battleTimer = a;
        battle.BattleTimer = _battleTimer;
        gameAction.OnBattleUpdateAction?.Invoke();
    }

    private void StartBattle() {
        ChangeBattleTimer(0f);
        foreach (ArtifactCardSO card in usedArtifactCardList) {
            artifactSo.Register(card);
        }
        _battleStatus = BattleStatus.Start;
        IdleBattle();
    }

    private void IdleBattle() {
        _battleStatus = BattleStatus.Idle;
        battleButtonText.SetText("暂停");
        ControlAllBattleUnitCard(UnitState.Idle);
    }

    private void PauseBattle() {
        _battleStatus = BattleStatus.Pasue;
        battleButtonText.SetText("继续");
        ControlAllBattleUnitCard(UnitState.Pause);
    }

    private void EndBattle() {
        _battleStatus = BattleStatus.End;
        battleButtonText.SetText("开始");
    }

    private void InitBattleUnitCards() {
        for (var i = 0; i < enemyBattleUnitCards.Length; ++i) {
            enemyBattleUnitCards[i].BattleUnitType = BattleUnitType.enemy;
            enemyBattleUnitCards[i].BattleUnitNo = i;
            if (i < enemyVacancyCards.Length) {
                enemyVacancyCards[i].battleUnitCard = enemyBattleUnitCards[i];
            }
            
            if (i < enemyBattleUnitUnitCardList.Count) {
                enemyBattleUnitCards[i].gameObject.SetActive(true);
                enemyBattleUnitCards[i].LinkCard(enemyBattleUnitUnitCardList[i]);
            } else {
                enemyBattleUnitCards[i].gameObject.SetActive(false);
            }
        }
        // ----
        for (var i = 0; i < allyBattleUnitCards.Length; ++i) {
            allyBattleUnitCards[i].BattleUnitType = BattleUnitType.ally;
            allyBattleUnitCards[i].BattleUnitNo = i + 10;
            if (i < allyVacancyCards.Length) {
                allyVacancyCards[i].battleUnitCard = allyBattleUnitCards[i];
            }
            
            if (i < allyBattleUnitUnitCardList.Count) {
                allyBattleUnitCards[i].gameObject.SetActive(true);
                allyBattleUnitCards[i].LinkCard(allyBattleUnitUnitCardList[i]);
            } else {
                allyBattleUnitCards[i].gameObject.SetActive(false);
            }
        }
    }

    private void UpdateBattleTimer() {
        if (BattleStatus.Idle != _battleStatus) return;
        ChangeBattleTimer(_battleTimer + Time.deltaTime);
    }

    private void Update() {
        UpdateBattleTimer();
        
        // cheat code
        if (Input.GetKeyDown (KeyCode.W)) {    
            cardUIEventChannel.RaiseRequestEvent(testCard, bottomHandCardRoot);
        }    
    }
}
