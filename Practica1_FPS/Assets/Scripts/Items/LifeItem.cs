using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItem : Item
{
    public override void CollectItem(Character character)
    {
        if (character.HasFullLife()) return;
        character.RefillLife();
        base.CollectItem(character);
    }
}
