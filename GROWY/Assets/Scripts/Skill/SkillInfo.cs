using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
    public string skillGUID;                // 고유 번호
    public int skillCode;                   // 참조할 스킬 코드
    public string skillName;                // 스킬 이름
    public string skillIconName;            // 스킬 아이콘의 이미지파일 이름
    public Sprite skillIcon;                // 스킬 아이콘 스프라이트
    public int skillLevel;                  // 스킬 레벨
    public int skillManaCost;               // 스킬 마나 코스트
    public float skillCoolTime;             // 스킬 쿨타임
    public string skillDescription;         // 스킬 설명
    public string soundName;                // 스킬 사운드 네임
    public SkillType skillType;             // 스킬 타입
    public SkillElement skillElement;       // 스킬 원소 속성

    public DamageInfo damageInfo = null;
    public BuffInfo buffInfo = null;
    
    // UI
    public int skillSlotIndex;              // 스킬 슬롯에서의 위치

    public SkillInfo(string _skillName, string _guid, int _skillCode, string _iconName, string _soundName, string _desc, SkillType _skillType, SkillElement _skillElement,
        int _cost, float _cooltime, int _skillLevel = 1)
    {
        skillGUID = _guid;
        skillCode = _skillCode;
        skillName = _skillName;
        skillIconName = _iconName;
        skillIcon = IconAtlasManager.Instance.skillIconAtlas.GetSprite(skillIconName);
        soundName = _soundName;
        skillDescription = _desc;
        skillType = _skillType;
        skillElement = _skillElement;
        skillManaCost = _cost;
        skillCoolTime = _cooltime;
        skillLevel = _skillLevel;
    }

    public SkillInfo(SkillInfo originInfo)
    {
        skillGUID = originInfo.skillGUID;
        skillCode = originInfo.skillCode;
        skillName = originInfo.skillName;
        skillIconName = originInfo.skillIconName;
        skillIcon = originInfo.skillIcon;
        soundName = originInfo.soundName;
        skillDescription = originInfo.skillDescription;
        skillType = originInfo.skillType;
        skillElement = originInfo.skillElement;
        skillManaCost = originInfo.skillManaCost;
        skillCoolTime = originInfo.skillCoolTime;
        skillLevel = originInfo.skillLevel;

        if (originInfo.damageInfo != null)
            damageInfo = originInfo.damageInfo;
        if (originInfo.buffInfo != null)
            buffInfo = originInfo.buffInfo;
    }

    public void SetNewGUID()
    {
        System.Guid myGUID = System.Guid.NewGuid();
        skillGUID = Utility.ByteArrayToString(myGUID.ToByteArray());
        skillGUID = skillGUID.Substring(0, 4);           // 32바이트 => 4바이트로 줄이기
    }

    public void InitDamageInfo(DamageType _dmType, int _fixedDM, int _scaleDM, int _hitCount)
    {
        damageInfo = new DamageInfo();
        damageInfo.damageType = _dmType;
        damageInfo.fixedDM = _fixedDM;
        damageInfo.scaleDM = _scaleDM;
        damageInfo.hitCount = _hitCount;
        damageInfo.hitTick = 0.2f;          // 이것도 나중에 DB로 저장하긴 해야됨...
    }

    public void InitBuffInfo(List<Option> _options, float _duration)
    {
        buffInfo = new BuffInfo();
        buffInfo.options = _options;
        buffInfo.duration = _duration;
    }
}
