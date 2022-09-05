using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorUp : Skill
{
    public override bool UseSkill(CardController cardController)
    {
        BattleManager.Instance.EventOnDefenceChange?.Invoke(cardController.CurrentArmor);
        
        return false;
    }
    public override ListOfSkills SkillType()
    {
        return ListOfSkills.ArmorUp;
    }
}
