using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : Item
{
    public override void CollectItem(Character character)
    {
        if (character.HasFullShield()) return;
        character.RefillShield();
        base.CollectItem(character);
    }
}
