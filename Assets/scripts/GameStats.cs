using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameStats : MonoBehaviour
{

    public Text flowersText;
    public Text flowersPerSecondText;
    public Text flowersPerClickTextTest;

    private System.Threading.Timer timer;

    public decimal flowers = 0.0m;
    public decimal handmadeFlowers = 0.0m;

    public decimal flowersPerClick = 1m;
    public decimal flowersPerClickMultiplier = 1.0m;
    public decimal flowersPerClickAddOn = 0.0m;
    public decimal flowersPerClickTotal;

    public decimal flowersPerSecond = 0.0m;

    public decimal flowersPerSecondMultiplier = 1.0m;
    public decimal flowersPerSecondTotal = 0.0m;

    public string handmadeFlowersString;
    public string flowersPerClickTotalString;

    int flowerAddPeriod = 25; // time between flowers updates in milliseconds

    Dictionary<int, string> zeroCountsToWords = new Dictionary<int, string>();

    public bool shortenNumbers = true;

    void Start()
    {
        timer = new System.Threading.Timer(UpdateProperty);
        timer.Change(flowerAddPeriod, flowerAddPeriod);

        zeroCountsToWords.Add(33, "decillion");
        zeroCountsToWords.Add(3, "thousand");
        zeroCountsToWords.Add(36, "undecillion");
        zeroCountsToWords.Add(6, "million");
        zeroCountsToWords.Add(39, "duodecillion");
        zeroCountsToWords.Add(9, "billion");
        zeroCountsToWords.Add(42, "tredecillion");
        zeroCountsToWords.Add(12, "trillion");
        zeroCountsToWords.Add(15, "quadrillion");
        zeroCountsToWords.Add(18, "quintillion");
        zeroCountsToWords.Add(21, "sextillion");
        zeroCountsToWords.Add(24, "septillion");
        zeroCountsToWords.Add(27, "octillion");
        zeroCountsToWords.Add(30, "nonillion");

        //		flowers = 123876544m; // testing
    }

    private void UpdateProperty(object state)
    {
        lock (this)
        {
            flowers += flowersPerSecondTotal / (1000.0m / flowerAddPeriod);
        }
    }

    void Update()
    {
        flowersPerClickTotal = flowersPerClick * flowersPerClickMultiplier + flowersPerClickAddOn;
        flowersPerSecondTotal = flowersPerSecond * flowersPerSecondMultiplier;
        string formattedFlowerCount = formatNumber(flowers, (shortenNumbers ? 3 : 0));
        flowersText.text = formattedFlowerCount + " flowers";
        flowersPerSecondText.text = "Per second: " + flowersPerSecondTotal.ToString(flowersPerSecondTotal == (int)flowersPerSecondTotal ? "N0" : "N1");

        flowersPerClickTextTest.text = "per click orig: " + flowersPerClick + "\nper click total: " + flowersPerClickTotal + "\nclick mult: " + flowersPerClickMultiplier + "\naddOn: " + flowersPerClickAddOn; // (test)

        handmadeFlowersString = handmadeFlowers.ToString();
        flowersPerClickTotalString = flowersPerClickTotal.ToString();
    }

    string spelledOutNumber(decimal num, int places)
    {
        if (num < 1000m)
            return Decimal.Round(num) + "";
        //		num = Decimal.Round(num / 10m) * 10m;
        num = Decimal.Round(num * (decimal)Math.Pow(10.0, (double)places)) / (decimal)Math.Pow(10.0, (double)places);
        string strNum = num + "";
        int headNumLen = strNum.Length % 3 == 0 ? 3 : strNum.Length % 3;
        int zerosLen = strNum.Length - headNumLen;
        float firstPart = Mathf.Round((float)num / Mathf.Pow(10f, (float)zerosLen) * Mathf.Pow(10f, (float)places)) / Mathf.Pow(10f, (float)places);
        return (firstPart == (int)firstPart ? (int)firstPart : firstPart) + " " + zeroCountsToWords[zerosLen];
    }

    public string formatNumber(decimal num, int places)
    {
        if (shortenNumbers)
            return spelledOutNumber(num, places);
        return num.ToString("#,##0." + new String('#', places));
    }
}