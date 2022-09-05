using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Skill
{   
    public override bool UseSkill(CardController cardController)
    {
        BattleManager.Instance.EventOnEnemyHealthChange?.Invoke(-cardController.CurrentAttack);
        return false;
    }

    public override ListOfSkills SkillType()
    {
        return ListOfSkills.Atack;
    }
}
