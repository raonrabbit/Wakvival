using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : LivingEntity
{
    public LayerMask whatIsTarget;
    public GameObject[] items;

    private LivingEntity target;
    private Animator monsterAnimator;
    private SpriteRenderer monsterRenderer;

    public float damage;
    public float speed;
    public float timeBetAttack = 0.5f;
    private float lastAttackTime;

    private bool hasTarget{
        get{
            if(target != null && !target.dead){
                return true;
            }
            return false;
        }
    }

    private void Awake(){
        monsterAnimator = GetComponent<Animator>();
        monsterRenderer = GetComponent<SpriteRenderer>();
    }

    //monster 셋업(체력, 데미지, 속도)
    public void Setup(MonsterData monsterData){
        maxHealth = monsterData.health;
        health = monsterData.health;
        damage = monsterData.damage;
        speed = monsterData.speed;
    }
    /*
    public void Setup(){
        maxHealth = 100f;
        health = 100f;
        damage = 10f;
        speed = 2f;
    }
    */
    private void Start(){
        StartCoroutine(UpdatePath());
    }

    private void Update(){
        monsterAnimator.SetBool("isWalking", hasTarget);

        //monster가 죽지 않았고 타겟이 있는 경우
        if(!dead && hasTarget){
        //타겟을 쫓아감
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    private IEnumerator UpdatePath(){
        while(!dead){
            //타겟이 존재할때
            if(hasTarget){
                //타겟이 monster 보다 오른쪽에 있을경우 flip
                if(target.transform.position.x > transform.position.x){
                    monsterRenderer.flipX = true;
                }
                //타겟이 monster 보다 왼쪽에 있을경우
                else{
                    monsterRenderer.flipX = false;
                }
            }

            //타겟이 존재하지 않을때
            else{
                //monster 근처에 있는 collider 중 whatIsTarget(Player) 레이어를 가지고 있는 개체를 찾음
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 20f, whatIsTarget);
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

    //데미지를 입었을 경우
    public override void OnDamage(float damage){
        if(!dead){
            //효과음, 이펙트 등등
        }
        base.OnDamage(damage);
    }

    //죽을 경우
    public override void Die(){
        base.Die();
        SpawnItem();
        Collider2D[] monsterColliders = GetComponents<Collider2D>();
        for(int i = 0; i < monsterColliders.Length; i++){
            monsterColliders[i].enabled = false;
        }
        monsterAnimator.SetTrigger("die");
        StopCoroutine(UpdatePath());
    }

    //아이템 드랍
    private void SpawnItem(){
        GameObject selectedItem = items[Random.Range(0, items.Length)];
        GameObject item = Instantiate(selectedItem, transform.position, Quaternion.identity);
    }


    //플레이어 공격
    private void OnTriggerStay2D(Collider2D other){
        if(!dead && Time.time >= lastAttackTime + timeBetAttack){
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
            if(attackTarget != null && attackTarget == target){
                lastAttackTime = Time.time;
                attackTarget.OnDamage(damage);
            }
        }
    }
}
