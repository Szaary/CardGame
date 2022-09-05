using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] private int _manaGainPerTurn;
    [SerializeField] private int _cardsGainPerTurn;
    [SerializeField] GameObject _player;
    [SerializeField] List<GameObject> _enemyPrefab = new List<GameObject>();
    [SerializeField] List<Transform> _enemyPositions = new List<Transform>();

    private BattleState _currentBattleState;
    public BattleState CurrentBattleState
    {
        get => _currentBattleState;
        private set => _currentBattleState = value;
    }

    public CardBattleEvents.EventOnHealthChange EventOnHealthChange;
    public CardBattleEvents.EventOnDamageDefenceOrHealthChange EventOnDamageDefenceOrHealthChange;
    public CardBattleEvents.EventOnManaChange EventOnManaChange;
    public CardBattleEvents.EventOnDefenceChange EventOnDefenceChange;
    public CardBattleEvents.EventOnEnemyHealthChange EventOnEnemyHealthChange;
    public CardBattleEvents.EventIsEnoughMana EventIsEnoughMana;
    public CardBattleEvents.EventDrawCards EventDrawCards;
    public CardBattleEvents.EventRemoveCards EventRemoveCards;
    public CardBattleEvents.EventOnEnemyDeath EventOnEnemyDeath;
    public CardBattleEvents.EventOnPlayerDeath EventOnPlayerDeath;
    public CardBattleEvents.EventInspectCard EventInspectCard;

    public enum BattleState
    {
        START,
        PLAYERTURN,
        ENEMYTURN,
        WON,
        LOST
    }

    private List<EnemyController> _enemyControllers = new List<EnemyController>();
    GameObject _enemy;
    protected override void Awake()
    {
        base.Awake();
        CurrentBattleState = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    void Start()
    {


        EventOnEnemyDeath.AddListener(HandleEnemyDeath);
        EventOnPlayerDeath.AddListener(HandlePlayerDeath);
    }

    private void HandlePlayerDeath(Player player)
    {
        //TODO Death Animation

        GameManager.Instance.RestartGame();
    }

    private IEnumerator SetupBattle()
    {
        yield return new WaitForSeconds(1);
        int i = 0;
        foreach (GameObject enemy in _enemyPrefab)
        {
            _enemy = Instantiate(enemy, _enemyPositions[i]);
            i++;
            _enemyControllers.Add(_enemy.GetComponent<EnemyController>());
        }

        CurrentBattleState = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentBattleState = BattleState.ENEMYTURN;
            EventRemoveCards?.Invoke(0);
            StartCoroutine(EnemyTurn());
        }
        
    }

    private void HandleEnemyDeath(EnemyController enemy)
    {
        _enemyControllers.Remove(enemy);
        enemy.gameObject.SetActive(false);
        if (_enemyControllers.Count == 0)
        {            
            Debug.Log("no enemies");
        }
    }

    private IEnumerator EnemyTurn()
    {
        foreach (EnemyController enemy in _enemyControllers)
        {
            enemy.Attack();
            yield return new WaitForSeconds(1);
        }
        _currentBattleState = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

    private IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(1);
        if (CurrentBattleState == BattleState.PLAYERTURN)
        {
            EventDrawCards?.Invoke(_cardsGainPerTurn);
            EventOnManaChange?.Invoke(_manaGainPerTurn);
        }

    }
}
