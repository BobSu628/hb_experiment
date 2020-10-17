using System;
using System.Collections.Generic;
using System.Linq;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[System.Serializable]
public class Model
{
    public long needsUpdate = 0;
    public UITab activeTab = UITab.Shell;
    
}
