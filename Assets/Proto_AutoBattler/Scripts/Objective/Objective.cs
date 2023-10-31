using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective
{
    [SerializeField] private int _maxHP = 5;
    [SerializeField] private int _currentHP = 5;
    [SerializeField] private int _chargeLevel = 0;
    [SerializeField] private bool _isTargetable; // Something like that if we want to add a cooldown
    public void SetObjectiveData(int maxHP, int currentHP, int chargeLevel)
    {
        _maxHP = maxHP;
        _currentHP = currentHP;
        _chargeLevel = chargeLevel;
    }

    public void incrementCharge(int inc)
    {
        _chargeLevel += inc;
    }

    public bool tryUseCharge(int usage)
    {
        if (_chargeLevel - usage <= 0)
            return false;
        _chargeLevel -= usage;
        return true;
    }

    private void DamageTaken(int dmg)
    {
        _currentHP -= dmg;
        if (_currentHP <= 0)
            GameManager.Instance.ObjectiveDead(); // Could also add VFX/SFX
    }
}
