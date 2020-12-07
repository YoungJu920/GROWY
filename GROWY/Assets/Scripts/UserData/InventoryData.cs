using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryType
{
    EQUIPMENT_BAG,
    CONSUMED_BAG,
    STONE_BAG,
    WEARING,
    CONSUMED_QUICK_SLOT,
    SKILL_BAG,
    SKILL_QUICK_SLOT
}


public class InventoryData
{
    private Dictionary<string, ItemInfo> Equip_Bag_List = new Dictionary<string, ItemInfo>();
    private Dictionary<string, ItemInfo> Consumed_Bag_List = new Dictionary<string, ItemInfo>();
    private Dictionary<string, ItemInfo> Stone_Bag_List = new Dictionary<string, ItemInfo>();
    private Dictionary<string, ItemInfo> Wearing_List = new Dictionary<string, ItemInfo>();
    private Dictionary<string, ItemInfo> Consumed_Quick_List = new Dictionary<string, ItemInfo>();

    private Dictionary<string, SkillInfo> Skill_Bag_List = new Dictionary<string, SkillInfo>();
    private Dictionary<string, SkillInfo> Skill_Quick_List = new Dictionary<string, SkillInfo>();

    public void Init(SimpleJSON.JSONNode list)
    {
        int x;
    }
}
