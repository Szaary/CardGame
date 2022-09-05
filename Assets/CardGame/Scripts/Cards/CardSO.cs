using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Card", menuName = "Card/Statistics")]
public class CardSO : ScriptableObject
{
    public new string name;
    public string description;

    [Range(0,10)]
    public int cost;
    [Range(-10, 10)]
    public int attack;
    [Range(-255, 10)]
    public int defence;

    public bool aoe;
    public bool canTargetEnemy;
    public bool canChangePlayerArmor;

    public List<ListOfSkills> listOfSkills = new List<ListOfSkills>();

    public Sprite sprite;
    public AudioClip audioClip;
    //public ParticleSystem particleSystem;
}
