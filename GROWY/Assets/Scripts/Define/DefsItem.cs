public struct Option
{
    public string optionName;
    public int optionValue;
    public int optionTier;
}

public enum ItemType
{
    Consumed,        // 소모 아이템
    Equip,           // 장비 아이템
    Stone                 // 스톤
}

public enum HealingType
{
    Immediately,    // 즉시 회복
    Constantly,      // 지속 회복
}

public enum HealingTarget
{
    HP,
    MP,
}

public enum StoneType
{
    NotStone = -1,
    Reality,
    Power,
    Mind,
    Time,
    Soul,
    Space,
}

public enum ItemRank
{
    CONSUMED = -2,
    STONE = -1,
    NORMAL,
    MAGIC,
    RARE,
    UNIQUE,
}

// 소비 아이템이 가지는 추가 정보
public class ConsumedItemInfo
{
    public HealingType healingType;
    public HealingTarget healingTarget;
    public int quantity;
    public float duration;
    public float coolTime;
}