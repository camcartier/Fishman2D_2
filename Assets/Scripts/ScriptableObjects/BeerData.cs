using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("BeerData"), menuName = ("Variable/BeerData"))]

public class BeerData : ScriptableObject

{
    public int _totalBeersInLvl1 = 1;
    public int _totalBeersInLvl2 = 3;
    public int _totalBeersInLvl3 = 6;

}
