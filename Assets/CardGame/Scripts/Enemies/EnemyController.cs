using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private EnemySO _enemy;

    private string _name;
    private int _attack;
    private int _defence;
    private int _maxHP;
    private int currentHP;

    [SerializeField] private GameObject _highlight;
    public int CurrentHP { get => currentHP; set => currentHP = value; }
    public string Name { get => _name; set => _name = value; }

    void Start()
    {
        Name = _enemy.name;
        _attack = _enemy.attack;
        _defence = _enemy.defence;
        _maxHP = _enemy.maxHP;
        CurrentHP = _enemy.currentHP;

     
    }

    public void Highlight(bool highlight)
    {
        _highlight.SetActive(highlight);
    }




    public void SubscribeToEvents(bool subscribe)
    {
        if (subscribe)
        {
            BattleManager.Instance.EventOnEnemyHealthChange.AddListener(ChangeHealth);
        }
        else
        {
            BattleManager.Instance.EventOnEnemyHealthChange.RemoveListener(ChangeHealth);
        }

    }

    public void Attack()
    {
        StartCoroutine(IAttack());
    }
    IEnumerator IAttack()
    {
        transform.position= new Vector3(transform.position.x, transform.position.y-3, transform.position.z);
        yield return new WaitForSeconds(1);
        transform.position =new Vector3(transform.position.x, transform.position.y+3, transform.position.z);
        BattleManager.Instance.EventOnDamageDefenceOrHealthChange?.Invoke(-_attack);
        
    }

    private void ChangeHealth(int heathChange)
    {
        CurrentHP += heathChange;
        if (CurrentHP <= 0)
        {
            BattleManager.Instance.EventOnEnemyDeath?.Invoke(this);
        }

    }

    private void OnDestroy()
    {
        //  BattleManager.Instance.EventOnEnemyHealthChange?.RemoveListener(ChangeHealth);
    }

}
