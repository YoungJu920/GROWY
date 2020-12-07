using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo
{
    public string itemGUID;             // 고유 번호 (MySQL)
    public int itemCode;                // 참조할 아이템 코드
    public string itemName;             // 아이템 이름
    public string itemDescription;      // 아이템 설명
    public int itemCount;               // 아이템 소지 개수
    public string itemIconName;         // 아이템 아이콘의 이미지파일 이름
    public Sprite itemIcon;             // 아이템 아이콘
    public ItemType itemType;           // 아이템 타입
    public int itemPrice;               // 아이템 가격
    public int itemLevel;               // 아이템 레벨
    public int itemPartsType;           // 아이템 부위
    public ItemRank itemRank;           // 아이템 등급

    public string targetSkillName;
    public int targetSkillCode;

    public ConsumedItemInfo consumedInfo = null;
    public StoneType stoneType;

    public List<Option> BaseOptionList = new List<Option>();
    public List<Option> AdditionalOptionList = new List<Option>();

    public ItemInfo(ItemType _itemType, string _guid, int _itemCode, string _itemName, string _iconName, int _partsType,
        int _itemLevel, string _desc, int _price, int _itemCount = 1, ItemRank _itemRank = ItemRank.NORMAL)
    {
        if (_guid == null)
        {
            System.Guid guid = System.Guid.NewGuid();
            itemGUID = Utility.ByteArrayToString(guid.ToByteArray());
            itemGUID = itemGUID.Substring(0, 4);           // 32바이트 => 4바이트로 줄이기
        }
        else
            itemGUID = _guid;

        itemType = _itemType;
        itemCode = _itemCode;
        itemName = _itemName;
        itemIconName = _iconName;
        itemCount = _itemCount;
        itemPartsType = _partsType;
        itemLevel = _itemLevel;
        itemDescription = _desc;
        itemRank = _itemRank;
        itemPrice = _price;
        itemIcon = IconAtlasManager.Instance.itemIconAtlas.GetSprite(itemIconName);
        stoneType = StoneType.NotStone;
    }

    public void SetNewGUID()
    {
        System.Guid myGUID = System.Guid.NewGuid();
        itemGUID = Utility.ByteArrayToString(myGUID.ToByteArray());
        itemGUID = itemGUID.Substring(0, 4);           // 32바이트 => 4바이트로 줄이기
    }

    // 깊은 복사용 생성자
    public ItemInfo(ItemInfo origin)
    {
        itemGUID = origin.itemGUID;
        itemType = origin.itemType;
        itemCode = origin.itemCode;
        itemName = origin.itemName;
        itemIconName = origin.itemIconName;
        itemCount = origin.itemCount;
        itemPartsType = origin.itemPartsType;
        itemLevel = origin.itemLevel;
        itemDescription = origin.itemDescription;
        itemRank = origin.itemRank;
        itemPrice = origin.itemPrice;
        itemIcon = IconAtlasManager.Instance.itemIconAtlas.GetSprite(itemIconName);
        stoneType = origin.stoneType;

        if (origin.consumedInfo != null)
        {
            ConsumedItemInfo consumed = origin.consumedInfo;
            InitConsumedInfo(consumed.healingType, consumed.healingTarget, consumed.quantity, consumed.duration, consumed.coolTime);
        }
    }

    public void InitConsumedInfo(HealingType _healingType, HealingTarget _healingTarget, int _quantity, float _duration, float _coolTime)
    {
        consumedInfo = new ConsumedItemInfo();
        consumedInfo.healingType = _healingType;
        consumedInfo.healingTarget = _healingTarget;
        consumedInfo.quantity = _quantity;
        consumedInfo.duration = _duration;
        consumedInfo.coolTime = _coolTime;
    }

    public void InitStoneInfo(StoneType _stoneType)
    {
        stoneType = _stoneType;
    }
}
