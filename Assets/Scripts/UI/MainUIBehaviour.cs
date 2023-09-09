using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUIBehaviour : MonoBehaviour
{
    [Header("Footer")]
    // [SerializeField] private TextMeshProUGUI  scrapText;
    // [SerializeField] private TextMeshProUGUI  nextTurretPriceText;
    // [SerializeField] private TextMeshProUGUI  currentWaveText;

    [SerializeField] private Nova.TextBlock scrapText;
    [SerializeField] private Nova.TextBlock nextTurretPriceText;
    [SerializeField] private Nova.TextBlock currentWaveText;
    
    public void SetScrapText(int scrap)
    {
        scrapText.TMP.text = scrap.ToString();
    }

    public void SetNextTurretPriceText(int price)
    {
        nextTurretPriceText.TMP.text = price.ToString();
    }

    public void SetCurrentWaveText(string waveName)
    {
        currentWaveText.TMP.text = waveName;
    }
}
