using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FloorSetting", menuName = "ScriptableObjects/FloorSetting")]
public class FloorSetting: ScriptableObject
{
    public int minX;
    public int minY;
    public int maxX;
    public int maxY;
    public List<FloorObjectSetting> floorObjects;
}

[Serializable]
public struct FloorObjectSetting{
    public int X;
    public int Y;
    public FloorGameObjectType FloorGameObjectType;
}
