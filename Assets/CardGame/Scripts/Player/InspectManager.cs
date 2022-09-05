using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;


    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _attackText;
    [SerializeField] private TextMeshProUGUI _defenceText;


    [SerializeField] private Sprite _sprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _cardDisplay;

    // Start is called before the first frame update
    void Start()
    {
        BattleManager.Instance.EventInspectCard.AddListener(ShowInspect);
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _cardDisplay.SetActive(false);
    }

    private void ShowInspect(CardController cardController, bool visible)
    {
        if (visible)
        {
            _cardDisplay.SetActive(true);
            _spriteRenderer.sprite = cardController.CurrentSprite;
            _nameText.text = cardController.CurrentName;
            _descriptionText.text = cardController.CurrentDescription;
            _sprite = cardController.CurrentSprite;
            _costText.text = cardController.CurrentCost.ToString();
            _attackText.text = cardController.CurrentAttack.ToString();
            _defenceText.text = cardController.CurrentArmor.ToString();
        }
        else
        {
            _cardDisplay.SetActive(false);
        }
    }



    // Update is called once per frame
    void Update()
    {

    }


}
