using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Character Stat Modifiers")]
public class CharacterStatModifiers : ScriptableObject
{
    [Min(0f)] public float healthMultiplier = 1;
    [Min(0f)] public float speedMultiplier = 1;
    [Min(0f)] public float damageMultipler = 1;
}
