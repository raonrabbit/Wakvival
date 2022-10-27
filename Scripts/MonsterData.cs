using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/MonsterData", fileName = "Monster Data")]
//몬스터 기본 데이터
public class MonsterData : ScriptableObject {
    public float health = 100f; // 체력
    public float damage = 10f; // 공격력
    public float speed = 1f; // 이동 속도
    public Color skinColor = Color.white; // 피부색
}