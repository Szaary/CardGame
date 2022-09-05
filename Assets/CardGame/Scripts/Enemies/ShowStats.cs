using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _nameText;

    private EnemyController _enemyController;

    void Start()
    {
        _enemyController = gameObject.GetComponentInParent<EnemyController>();
    }
    private void Update()
    {
        _healthText.text = _enemyController.CurrentHP.ToString();
        _nameText.text = _enemyController.Name;
    }
}
