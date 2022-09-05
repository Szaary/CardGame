using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : CardBase
{
    
    private Camera _mainCamera;
    private EnemyController _collidedEnemy;
    private bool _isEnemySelected = false;
    private bool _isInDropArea = false;

    private Player _player;
    private Vector3 _startingPosition;
    private Vector3 _baseTransform;

    [SerializeField] private float _cardAnimationTime=0f;
    

    private void OnMouseEnter()
    {
        InvokeInspectEvent(true);
        transform.localScale *= 1.05f;
    }

    private void OnMouseExit()
    {
        InvokeInspectEvent(false);
        transform.localScale= _baseTransform; 
    }

    public void OnMouseDrag()
    {
        InvokeInspectEvent(false);
        _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }

    public void OnMouseUp()
    {
        transform.position = _startingPosition;
    }


    private void OnMouseDown()
    {
        _startingPosition = transform.position;
    }



    protected override void Start()
    {
        _baseTransform = transform.localScale;

        base.Start();

        _mainCamera = FindObjectOfType<Camera>();
        _player = FindObjectOfType<Player>();

        CurrentAttack = _card.attack + _player.attack;
        CurrentArmor = _card.defence + _player.armor;
    }

    private void Update()
    {
        if (BattleManager.Instance.CurrentBattleState == BattleManager.BattleState.PLAYERTURN)
        {
            CardActionOnPlayerTurn();
        }
    }

    private void InvokeInspectEvent(bool visible)
    {
        BattleManager.Instance.EventInspectCard?.Invoke(this, visible);
    }

    private void CardActionOnPlayerTurn()
    {

        if (CurrentCost <= _player.mana)
        {
            BattleManager.Instance.EventIsEnoughMana?.Invoke(true);
            if (Input.GetMouseButtonUp(0))
            {
                if (_isInDropArea && _card.aoe && _card.canTargetEnemy)
                {
                    StartCoroutine(AOEAttack());
                }
                else if (_isEnemySelected && _card.canTargetEnemy)
                {
                    StartCoroutine(SingleTargetAttack());
                }
                else if (_card.canTargetEnemy == false && _isInDropArea)
                {
                    StartCoroutine(NonEnemyEffect());
                }
            }
        }
        else
        {

            BattleManager.Instance.EventIsEnoughMana?.Invoke(false);

        }
    }

    IEnumerator NonEnemyEffect()
    {
        BattleManager.Instance.EventOnManaChange?.Invoke(-CurrentCost);
        foreach (Skill skill in _skills)
        {
            skill.UseSkill(this);
        }
        PlayCardAudio();
        ParticleSystemPlay();
        
        yield return new WaitForSeconds(_cardAnimationTime);
        gameObject.SetActive(false);
    }

    IEnumerator SingleTargetAttack()
    {
        BattleManager.Instance.EventOnManaChange?.Invoke(-CurrentCost);
        _collidedEnemy.SubscribeToEvents(true);
        foreach (Skill skill in _skills)
        {
            skill.UseSkill(this);
        }
        _collidedEnemy.SubscribeToEvents(false);
        RemoveHighlightFromEnemy(_collidedEnemy.GetComponent<Collider2D>());
        PlayCardAudio();
        ParticleSystemPlay();
        yield return new WaitForSeconds(_cardAnimationTime);
        gameObject.SetActive(false);
    }

    IEnumerator AOEAttack()
    {
        BattleManager.Instance.EventOnManaChange?.Invoke(-CurrentCost);

        List<EnemyController> enemies = GetListOfEnemies();

        foreach (EnemyController enemy in enemies)
        {
            enemy.SubscribeToEvents(true);

            foreach (Skill skill in _skills)
            {
                skill.UseSkill(this);
            }
            enemy.SubscribeToEvents(false);
        }
        PlayCardAudio();
        ParticleSystemPlay();
        RemoveEnemiesFromHiglight();
        yield return new WaitForSeconds(_cardAnimationTime);
               
        gameObject.SetActive(false);

    }

    private void PlayCardAudio()
    {
  
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = CurrentAudioClip;
        audioSource.Play();        
    }

    private void ParticleSystemPlay()
    {
        //TODO Make it work
        //_particleSystem.Play();
        
    }

    private static List<EnemyController> GetListOfEnemies()
    {
        List<EnemyController> enemies = new List<EnemyController>();
        enemies.AddRange(FindObjectsOfType<EnemyController>());
        return enemies;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            _isEnemySelected = true;
            _collidedEnemy = collision.gameObject.GetComponent<EnemyController>();

            if (CurrentCanTargetEnemy && !_card.aoe)
            {
                HighlightEnemy(collision);
            }

        }
        if (collision.CompareTag("DropArea"))
        {
            if (_card.aoe && CurrentCanTargetEnemy)
            {
                List<EnemyController> enemies = GetListOfEnemies();
                foreach (EnemyController enemy in enemies)
                {
                    HighlightEnemy(enemy.GetComponent<Collider2D>());
                }
            }

            _isInDropArea = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _isEnemySelected = false;
            _collidedEnemy = null;
            if (CurrentCanTargetEnemy && !_card.aoe)
            {
                RemoveHighlightFromEnemy(collision);
            }
        }
        if (collision.CompareTag("DropArea"))
        {
            RemoveEnemiesFromHiglight();
            _isInDropArea = false;
        }
    }

    private void RemoveEnemiesFromHiglight()
    {
        if (_card.aoe && CurrentCanTargetEnemy)
        {
            List<EnemyController> enemies = GetListOfEnemies();
            foreach (EnemyController enemy in enemies)
            {
                RemoveHighlightFromEnemy(enemy.GetComponent<Collider2D>());
            }
        }
    }
    //TODO BUG WITH Showing highlights... 
    private void HighlightEnemy(Collider2D collider)
    {
        //BattleManager.Instance.EventOnEnemyHighlight?.Invoke(true);
        collider.gameObject.GetComponent<EnemyController>().Highlight(true);
    }
    private void RemoveHighlightFromEnemy(Collider2D collider)
    {
        // BattleManager.Instance.EventOnEnemyHighlight?.Invoke(false);
        collider.gameObject.GetComponent<EnemyController>().Highlight(false);

    }
}
