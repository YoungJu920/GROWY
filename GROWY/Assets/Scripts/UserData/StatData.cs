using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatData
{
    private Dictionary<string, float> statDictionary = new Dictionary<string, float>();

    // JSONNode -> Dictionary
    public void Init(SimpleJSON.JSONNode list)
    {
        foreach (var node in list)
        {
            statDictionary.Add(node.Key, node.Value);
        }
    }
}
