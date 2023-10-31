using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Tooltip("Initial stats for the character")]
    [SerializeField] private CharacterStats _characterStats;
    [Header("---------- Optional components ----------")]
    [Tooltip("Multiplicative stat modifiers (could also be additive later on)")]
    [SerializeField] private CharacterStatModifiers _characterStatModifiers;

    public void SpawnCharacter(Vector3 position)
    {
        // TODO : We should evaluate that before spawning the character
        if (_characterStatModifiers != null)
            _characterStats.ApplyStatsModifiers(_characterStatModifiers);
        var newCharacter = Instantiate(_characterStats.instantiationPrefab, position, Quaternion.identity);
        Type characterType = _characterStats.instantiationPrefab
        newCharacter.GetComponent<characterType>() = this;

    }
}