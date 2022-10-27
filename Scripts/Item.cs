using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //몬스터가 아이템을 떨어뜨린 후 아이템 위로 살짝 튕기는 느낌 주기 위해 사용
    private float time;
    private float speed = 0.25f;

    //아이템이 플레이어를 따라가는 속도
    private float eatspeed = 5f;

    //아이템이 플레이어를 인지하고 따라갈 수 있는 최대 거리
    public int magneticLange;

    //타겟의 LivingEntity 속성
    private LivingEntity target;

    private void Start(){
        time = 0;
        magneticLange = 1;
        StartCoroutine(UpdatePath());
    }

    private void Update(){
        //아이템 위로 튕기는 효과
        if(time < 0.1f){
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1f), Time.deltaTime * speed * 15);
        }
        else if(time < 1f){
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - 1f), Time.deltaTime * speed);
        }

        //타겟이 있을경우 타겟을 따라가도록 함
        if(hasTarget){
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, eatspeed * Time.deltaTime);
        }

        time += Time.deltaTime;
    }

    //타겟 유무
    private bool hasTarget{
        get{
            if(target != null && !target.dead){
                return true;
            }
            return false;
        }
    }

    //타겟을 지속적으로 찾는 Coroutine
    private IEnumerator UpdatePath(){
        while(true){
            if(!hasTarget)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, magneticLange, LayerMask.GetMask("Player"));
                for(int i = 0; i < colliders.Length; i++){
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();
                    
                    if(livingEntity != null && !livingEntity.dead){
                        target = livingEntity;
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

}
