using System.Collections.Generic;

public enum SkillType
{
    근거리,               // 근거리 스킬
    원거리,              // 원거리 스킬
    투사체,              // 투사체 스킬
    버프,                 // 버프 스킬
    광역,                  // 상하좌우 광역
    저주              // 디버프
}

public enum SkillElement
{
    무,
    화염,
    냉기,
    빛,
    어둠,
}

public enum DamageType
{
    AD,             // 물리 공격
    AP,             // 주문 공격
}

public class BuffInfo
{
    public List<Option> options;          // 증가 시킬 옵션 리스트 (옵션 이름, 옵션 벨류)
    public float duration;                // 지속 시간
}

public class DamageInfo
{
    public DamageType damageType;
    public int fixedDM;
    public int scaleDM;
    public int hitCount;
    public float hitTick;
}