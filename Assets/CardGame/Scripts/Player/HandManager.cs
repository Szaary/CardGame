using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    private CardPooler cardPooler;

    void Start()
    {
        cardPooler = GetComponent<CardPooler>();

        BattleManager.Instance.EventDrawCards.AddListener(DrawCards);
        BattleManager.Instance.EventRemoveCards.AddListener(RemoveCards);
    }

    private void RemoveCards(int ammount)
    {
        if (ammount == 0)
        {
            cardPooler.DespawnAllObjects();
        }
    }

    private void DrawCards(int ammount)
    {       
        for(int i = 0; i < ammount; i++)
        {
            cardPooler.SpawnObjectFromPool(true, gameObject, new Vector3(0,0,0));
        }        
    }
}
