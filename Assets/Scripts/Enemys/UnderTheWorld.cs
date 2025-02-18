using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderTheWorld : Enemy
{
    override
    public void DestroyEnemy(bool instant)
    {
        GameManager.Instance.EndGame();
    }
}
