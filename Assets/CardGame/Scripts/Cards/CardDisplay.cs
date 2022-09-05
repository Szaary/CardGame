using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CardDisplay : MonoBehaviour
{
    private CardController _cardController;

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;


    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _attackText;
    [SerializeField] private TextMeshProUGUI _defenceText;


    [SerializeField] private Sprite _sprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    void Start()
    {

        _cardController = gameObject.GetComponentInParent<CardController>();

        if (_cardController.CurrentCanTargetEnemy == false)
        {
            _attackText.gameObject.SetActive(false);
        }
        else
        {
            _attackText.gameObject.SetActive(true);
        }

        if (_cardController.CurrentCanChangePlayerArmor == false)
        {            
            _defenceText.gameObject.SetActive(false);
        }
        else
        {
            _defenceText.gameObject.SetActive(true);
        }

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //_spriteRenderer.sprite = _sprite;
        BattleManager.Instance.EventIsEnoughMana.AddListener(ChangeManaColor);
    }

    private void ChangeManaColor(bool isEnoughMana)
    {
        if (isEnoughMana)
        {
            _costText.color = new Color(0, 0, 0);
        }
        else if(!isEnoughMana && (_cardController.CurrentCost != 0))
        {
            _costText.color = new Color(111, 0, 0);
        }
    }

    private void Update()
    {
        // TODO change to event 
        _spriteRenderer.sprite = _cardController.CurrentSprite;
        _nameText.text = _cardController.CurrentName;
        _descriptionText.text = _cardController.CurrentDescription;
        _sprite= _cardController.CurrentSprite;
        _costText.text = _cardController.CurrentCost.ToString();
        _attackText.text = _cardController.CurrentAttack.ToString();
        _defenceText.text = _cardController.CurrentArmor.ToString();
    }
}
