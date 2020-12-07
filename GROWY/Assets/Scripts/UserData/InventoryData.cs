using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryType
{
    EQUIPMENT_BAG,
    CONSUMED_BAG,
    STONE_BAG,
    SKILL_BAG,
    WEARING,
    CONSUMED_QUICK_SLOT,
    SKILL_QUICK_SLOT
}

public class InventoryData
{
    private Dictionary<string, ItemInfo> EquipItemList = new Dictionary<string, ItemInfo>();
    private Dictionary<string, ItemInfo> ConsumedItemList = new Dictionary<string, ItemInfo>();
    private Dictionary<string, ItemInfo> StoneItemList = new Dictionary<string, ItemInfo>();
}
