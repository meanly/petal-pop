using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingButton : MonoBehaviour
{

    public GameStats gameStats;

    public float priceIncrease = 1.15f;
    public int count = 0;

    public string myName;
    public long price;
    public double flowersPerSecond;
    public double flowersPerSecondMultiplier = 1.0;
    public double flowersPerSecondAddOn = 0.0;

    public bool isButtonVisible;
    public bool isNameVisible;

    public double getTotalFlowersPerSecondCombined()
    {
        return count * getTotalFlowersPerSecond();
    }

    public double getTotalFlowersPerSecond()
    {
        return flowersPerSecond * flowersPerSecondMultiplier + flowersPerSecondAddOn;
    }

    public string getPluralName()
    {
        return myName == "factory" ? "factories" : myName + "s";
    }

    public string getNameSingularOrPlural()
    {
        return count == 1 ? myName : getPluralName();
    }

    public string getProductionDescriptionTotal()
    {
        return count + " " + getNameSingularOrPlural() + " producing " + getTotalFlowersPerSecondCombined().ToString("#,##0.##") + " flower" + (getTotalFlowersPerSecondCombined() == 1.0 ? "" : "s") + " per second";
    }

    public string getProductionDescriptionSingle()
    {
        return "each " + myName + " produces " + getTotalFlowersPerSecond().ToString("#,##0.##") + " flowers per second";
    }

}