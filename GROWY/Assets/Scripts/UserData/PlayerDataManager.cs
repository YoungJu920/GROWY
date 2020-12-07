using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LoadPlayerStatReturnCode
{
    SUCCESS = 0,
    NO_EXIST_USER_INDEX = 1
}

public enum LoadPlayerInventoryReturnCode
{
    SUCCESS = 0,
    NO_EXIST_USER_INDEX = 1
}

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    private StatData statData {get; set;}
    private InventoryData inventoryData {get; set;}

    private int user_index;

    public void Init()
    {
        statData = new StatData();
        inventoryData = new InventoryData();

        user_index = PlayerPrefs.GetInt("user_index");

        LoadPlayerStat();
        LoadPlayerInventory();
    }

    void LoadPlayerStat()
    {
        ServerManager.Instance.Request("LOAD_PLAYER_STAT", new string[]{user_index.ToString()}, ResultOfLoadPlayerStat, true);
    }

    void LoadPlayerInventory()
    {
        ServerManager.Instance.Request("LOAD_PLAYER_INVENTORY", new string[]{user_index.ToString()}, ResultOfLoadPlayerInventory, true);
    }

    void ResultOfLoadPlayerStat(string result)
    {
        var list = Utility.ParsingString(result);

        if (list == null)
            return;

        LoadPlayerStatReturnCode return_code = (LoadPlayerStatReturnCode)(list["return_code"].AsInt);

        switch(return_code)
        {
            case LoadPlayerStatReturnCode.NO_EXIST_USER_INDEX:
            {
                Debug.Log("NO_EXIST_USER_INDEX : " + user_index);
                break;
            }

            case LoadPlayerStatReturnCode.SUCCESS:
            {
                list.Remove("return_code");
                list.Remove("user_index");

                statData.Init(list);
                break;
            }
        }
    }

    void ResultOfLoadPlayerInventory(string result)
    {
        var list = Utility.ParsingString(result);

        if (list == null)
            return;

        LoadPlayerInventoryReturnCode return_code = (LoadPlayerInventoryReturnCode)(list["return_code"].AsInt);

        switch(return_code)
        {
            case LoadPlayerInventoryReturnCode.NO_EXIST_USER_INDEX:
            {
                Debug.Log("NO_EXIST_USER_INDEX : " + user_index);
                break;
            }

            case LoadPlayerInventoryReturnCode.SUCCESS:
            {
                list.Remove("return_code");

                inventoryData.Init(list);
                break;
            }
        }
    }
}
