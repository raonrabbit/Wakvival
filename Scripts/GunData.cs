using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/GunData", fileName = "Gun Data")]

//총의 기본 데이터
public class GunData : ScriptableObject
{
    //public AudioClip shotClip;
    //public AudioClip reloadClip;

    public float damage = 50;

    public float timeBetFire = 0.5f;
}
