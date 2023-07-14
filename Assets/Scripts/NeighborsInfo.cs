using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborsInfo
{
    public readonly int sameNeighborsCount;
    public readonly List<int> oppositeNeighborsWeights;

    public NeighborsInfo(int sameNeighborsCount, List<int> oppositeNeighborsWeights)
    {
        this.sameNeighborsCount = sameNeighborsCount;
        this.oppositeNeighborsWeights = oppositeNeighborsWeights;
    }
}
