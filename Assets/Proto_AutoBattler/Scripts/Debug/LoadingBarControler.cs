using System.Collections;
using System.Collections.Generic;
using Nova;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LoadingBarControler : MonoBehaviour
{
    [SerializeField] private UIBlock2D fillingBar;
    [SerializeField] private TextMeshPro cdNameText;  
    [SerializeField] private TextMeshPro timeLeftText;

    private float cooldownDuration;

    public void StartLoadingBarUI(float duration, string name)
    {
        if (duration <= 0)
            duration = 0.1f;
        cooldownDuration = duration;
        cdNameText.text = name;
    }
    
    public void UpdateLoadingBarUI(float timePassed)
    {
        timeLeftText.text = (cooldownDuration - timePassed).ToString();
        float percentFilled = Mathf.Clamp01(timePassed / cooldownDuration);

        fillingBar.Size.X.Percent = percentFilled;
    }
}
