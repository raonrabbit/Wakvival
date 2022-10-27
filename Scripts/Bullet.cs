using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//총알 구현

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    //총알의 속도

    private float damage;
    //총알의 데미지

    private Rigidbody2D bulletRigidbody;
    //총알의 rigidbody

    private SpriteRenderer rend;
    //총알의 렌더러

    void Start()
    {
        damage = GameObject.Find("Gun").GetComponent<Gun>().gunData.damage;
        //Gun 오브젝트를 찾아 해당 오브젝트에서 damage 수치를 불러온다.

        bulletRigidbody = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

        //총이 반전되어 있다면 총알의 렌더러도 반전, 우측으로 speed 만큼의 속력을 가한다.
        if(WeaponRotate.isFlip){
            rend.flipX = true;
            bulletRigidbody.velocity = transform.right * speed;
        }
        
        //총이 반전되어 있지 않을 경우, 좌측으로 speed 만큼의 속력을 가한다.
        else{
            rend.flipX = false;
            bulletRigidbody.velocity = -transform.right * speed;
        }

        Destroy(gameObject, 1.5f);
        //발사되고 1.5초 뒤 파괴
    }


    //총알이 무언가에 닿았을 때
    private void OnTriggerEnter2D(Collider2D other){
            //닿은 오브젝트의 LivingEntity 정보를 가져옴
            LivingEntity target = other.GetComponent<LivingEntity>();

            //레이어가 Enemy이고 죽은 상태가 아닐경우 데미지를 가함
            if(other.gameObject.layer == LayerMask.NameToLayer("Enemy") && target != null && !target.dead){
                target.OnDamage(damage);
                Destroy(gameObject);
                //오브젝트 파괴
            }
        }
}
