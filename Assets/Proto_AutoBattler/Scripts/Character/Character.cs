using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Tooltip("Initial stats for the character")]
    [SerializeField] private CharacterStats _characterStats;
    [Header("---------- Optional components ----------")]
    [Tooltip("Multiplicative stat modifiers, can also be set to additive")]
    [SerializeField] private CharacterStatModifiers _characterStatModifiers;
}