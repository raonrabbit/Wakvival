using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    //총알 프리팹

    public Transform fireLeftTransform;
    //왼쪽 총구 위치

    public Transform fireRightTransform;
    //오른쪽 총구 위치

    public GunData gunData;
    //총의 기본 데이터

    float lastFireTime;
    //마지막 사격 시간

    private void OnEnable(){
        lastFireTime = 0;
    }

    //마지막 사격시간에서 timeBetFire 이상 지났을때만 Shot() 수행
    public void Fire(){
        if(Time.time >= lastFireTime + gunData.timeBetFire){
            lastFireTime = Time.time;
            Shot();
        }
    }

    //총알 프리팹을 Instantiate하고 발사
    public void Shot(){
        if(WeaponRotate.isFlip){
            Instantiate(bulletPrefab, fireRightTransform.position, fireRightTransform.rotation);
        }
        else{
            Instantiate(bulletPrefab, fireLeftTransform.position, fireLeftTransform.rotation);
        }
    }

    void Update()
    {
         if(Input.GetMouseButton(0)){
            Fire();
         }
    }
}
