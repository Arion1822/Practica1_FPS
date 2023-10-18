using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : Item
{
    public override void CollectItem(Character character)
    {
        if (character.HasFullAmmo()) return;
        character.RefillAmmo();
        base.CollectItem(character);
    }
}
