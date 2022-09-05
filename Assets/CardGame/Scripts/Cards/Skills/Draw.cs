using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : Skill
{   
    public override bool UseSkill(CardController cardController)
    {
        BattleManager.Instance.EventDrawCards?.Invoke(1);
        return false;
    }

    public override ListOfSkills SkillType()
    {
        return ListOfSkills.Draw;
    }
}
