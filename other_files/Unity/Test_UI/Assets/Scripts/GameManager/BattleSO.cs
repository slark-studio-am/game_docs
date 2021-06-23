using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSO : ScriptableObject {
    [TextArea] [SerializeField] private string description = default;
    // 敌方卡槽 UI层
    [SerializeField] private List<BattleUnitCard> enemyBattleUnitCards = default;
    // 我方卡槽 UI层
    [SerializeField] private List<BattleUnitCard> allyBattleUnitCards = default;

    public List<BattleUnitCard> EnemyBattleUnitCards => enemyBattleUnitCards;
    public List<BattleUnitCard> AllyBattleUnitCards => allyBattleUnitCards;

    public float BattleTimer = default;

    public void SetBattleUnitCards(BattleUnitCard[] enemy, BattleUnitCard[] ally) {
        enemyBattleUnitCards.Clear();
        enemyBattleUnitCards.AddRange(enemy);
        
        allyBattleUnitCards.Clear();
        allyBattleUnitCards.AddRange(ally);
    }
}
