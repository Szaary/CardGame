using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase : MonoBehaviour
{
    [SerializeField] protected CardSO _card;
    protected List<Skill> _skills = new List<Skill>();

    private int _currentAttack;
    private int _currentArmor;
    private int _currentCost;
    private string _currentName;
    private string _currentDescription;
    private bool _canTargetEnemy;
    private bool _canChangePlayerArmor;
    private Sprite _sprite;
    [SerializeField]private AudioClip _audioClip;


    public ParticleSystem _particleSystem;

    protected Skill[] _tableOfAllSkills;

    public int CurrentAttack { get => _currentAttack; protected set => _currentAttack = value; }
    public int CurrentArmor { get => _currentArmor; protected set => _currentArmor = value; }
    public int CurrentCost { get => _currentCost; protected set => _currentCost = value; }
    public string CurrentName { get => _currentName; protected set => _currentName = value; }
    public string CurrentDescription { get => _currentDescription; protected set => _currentDescription = value; }
    public bool CurrentCanTargetEnemy { get => _canTargetEnemy; protected set => _canTargetEnemy = value; }
    public bool CurrentCanChangePlayerArmor { get => _canChangePlayerArmor; protected set => _canChangePlayerArmor = value; }
    public Sprite CurrentSprite { get => _sprite; set => _sprite = value; }
    public AudioClip CurrentAudioClip { get => _audioClip; set => _audioClip = value; }

     public ParticleSystem CurrentParticleSystem { get => _particleSystem; set => _particleSystem = value; }


    protected virtual void Start()
    {
        CurrentCost = _card.cost;
        CurrentName = _card.name;
        CurrentDescription = _card.description;
        CurrentCanTargetEnemy = _card.canTargetEnemy;
        CurrentCanChangePlayerArmor = _card.canChangePlayerArmor;
        //CurrentParticleSystem = _card.particleSystem;
        CurrentSprite = _card.sprite;
        CurrentAudioClip = _card.audioClip;
        GetDeckOfCard();
    }

    private void GetDeckOfCard()
    {
        // Move it to deck Create
        _tableOfAllSkills = Resources.LoadAll<Skill>("Prefabs/Skills");
        foreach (ListOfSkills skill in _card.listOfSkills)
        {
            foreach (Skill tableSkill in _tableOfAllSkills)
            {
                if (skill == tableSkill.SkillType())
                {
                    _skills.Add(tableSkill);
                }
            }
        }
    }
}