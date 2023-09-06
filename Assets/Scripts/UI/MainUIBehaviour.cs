using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUIBehaviour : MonoBehaviour
{
    [Header("Footer")]
    [SerializeField] private TextMeshProUGUI  scrapText;
    [SerializeField] private TextMeshProUGUI  nextTurretPriceText;
    [SerializeField] private TextMeshProUGUI  currentWaveText;

    public void SetScrapText(int scrap)
    {
        scrapText.text = scrap.ToString();
    }

    public void SetNextTurretPriceText(int price)
    {
        nextTurretPriceText.text = price.ToString();
    }

    public void SetCurrentWaveText(string waveName)
    {
        currentWaveText.text = waveName;
    }
}
