using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "New Playable Character/Skill")]
public class Skill : ScriptableObject
{
    public string skillname; // 스킬 이름
    public float damage; // 스킬 데미지
    public float cooldown; // 스킬 쿨타임
    public float moveRange; // 스킬 이동 범위
    public AnimationClip clip; // 스킬 애니매이션 클립
    public GameObject summonPrefab; // 스킬 소환 프리팹
    [TextArea]
    public string skillDesc; // 스킬 설명
}