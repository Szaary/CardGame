using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : UIManagerBase
{
    protected override void Update()
    {
        base.Update();
        StartGame("BattleScene");
    }

}
