using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _manaText;
    [SerializeField] private TextMeshProUGUI _attackText;
    [SerializeField] private TextMeshProUGUI _armorText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _defenceText;

    [SerializeField] private Player _player;

    private void Start()
    {
        _manaText.text = "Mana: " + _player.mana.ToString();
        _healthText.text = _player.currentHP.ToString() + " :Health";
        _armorText.text = "Armor: " + _player.armor.ToString();
        _attackText.text = "Attack: " + _player.attack.ToString();
        _defenceText.text = _player.Defence.ToString() + " :Defence";

        BattleManager.Instance.EventOnDamageDefenceOrHealthChange.AddListener(ChangeDefenceOrHealth);
        BattleManager.Instance.EventOnManaChange.AddListener(ChangeMana);
        BattleManager.Instance.EventOnDefenceChange.AddListener(ChangeDefence);
    }


    private void Update()
    {
        _manaText.text = "Mana: " + _player.mana.ToString();
        _healthText.text = _player.currentHP.ToString() + " :Health";
        _armorText.text = "Armor: " + _player.armor.ToString();
        _attackText.text = "Attack: " + _player.attack.ToString();
        _defenceText.text = _player.Defence.ToString() + " :Defence";
    }

    private void ChangeDefence(int arg0)
    {
        _defenceText.text = _player.Defence.ToString() + " :Defence";
    }

    private void ChangeMana(int arg0)
    {
        _manaText.text = "Mana: " + _player.mana.ToString();
    }

    private void ChangeDefenceOrHealth(int arg0)
    {
        _defenceText.text = _player.Defence.ToString() + " :Defence";
        _healthText.text = _player.currentHP.ToString() + " :Health";
    }

    protected void OnDestroy()
    {
        //BattleManager.Instance.EventOnManaChange.RemoveListener(ChangeMana);
        //BattleManager.Instance.EventOnHealthChange.RemoveListener(ChangeHealth);
        //BattleManager.Instance.EventOnManaChange.RemoveListener(ChangeDefence);
        //base.OnDestroy();
    }

}
