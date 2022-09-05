using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public abstract bool UseSkill(CardController cardController);
    public abstract ListOfSkills SkillType();
}
