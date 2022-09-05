using UnityEngine.Events;

public class CardBattleEvents : Events
{

    // Player Events
    [System.Serializable] public class EventOnHealthChange : UnityEvent<int> { }
    [System.Serializable] public class EventOnDamageDefenceOrHealthChange : UnityEvent<int> { }
    [System.Serializable] public class EventOnManaChange : UnityEvent<int> { }
    [System.Serializable] public class EventOnDefenceChange : UnityEvent<int> { }
    [System.Serializable] public class EventOnPlayerDeath : UnityEvent<Player> { }

    // Enemies events
    [System.Serializable] public class EventOnEnemyHealthChange : UnityEvent<int> { }
    [System.Serializable] public class EventOnEnemyDeath : UnityEvent <EnemyController> {}

    // Card events
    [System.Serializable] public class EventIsEnoughMana : UnityEvent<bool> {}
    [System.Serializable] public class EventDrawCards : UnityEvent<int> { }
    [System.Serializable] public class EventRemoveCards : UnityEvent<int> { }
    
    [System.Serializable] public class EventInspectCard : UnityEvent<CardController, bool> { }

}
