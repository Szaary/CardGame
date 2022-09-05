using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public new string name;
    public int attack;
    public int defence;

    public int maxHP;
    public int currentHP;

}
