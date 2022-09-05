using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public int attack;
    public int armor;

    [SerializeField] private int _defence;
    public int maxHP;
    public int currentHP;

    public int mana;
    public int Defence { get => _defence; set => _defence = value; }

    private void Start()
    {
        BattleManager.Instance.EventOnDamageDefenceOrHealthChange.AddListener(ChangeDefenceOrHealth);
        BattleManager.Instance.EventOnManaChange.AddListener(ChangeMana);
        BattleManager.Instance.EventOnDefenceChange.AddListener(ChangeDefence);
    }

    private void ChangeMana(int manaChange)
    {
        mana += manaChange;
    }

    public void ChangeDefenceOrHealth(int healthChange)
    {
        
        if (-healthChange < Defence) Defence += healthChange;
        else if (Defence > 0)
        {
            healthChange += Defence;
            currentHP += healthChange;
            Defence = 0;
        }
        else currentHP += healthChange;

        if (currentHP <= 0) 
        {
            BattleManager.Instance.EventOnPlayerDeath?.Invoke(this);
        }
    }

    public void ChangeDefence(int armorChange)
    {
        _defence += armorChange;
    }

    protected override void OnDestroy()
    {
        //BattleManager.Instance.EventOnManaChange.RemoveListener(ChangeMana);
        //BattleManager.Instance.EventOnHealthChange.RemoveListener(ChangeHealth);
        //BattleManager.Instance.EventOnManaChange.RemoveListener(ChangeDefence);
        base.OnDestroy();
    }

}
