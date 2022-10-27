using System;
using UnityEngine;

//살아있는것에 모두 부여되는 코드
public class LivingEntity : MonoBehaviour, IDamageable
{
    //최대 체력
    public float maxHealth = 500f;

    //체력
    public float health{get; protected set;}

    //죽었을 경우
    public bool dead{get; protected set;}
    public event Action onDeath;

    //이 오브젝트의 Rigidbody
    private Rigidbody2D rigid;

    void Start(){
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable(){
        dead = false;
        health = maxHealth;
    }

    //피해를 입었을 경우 damage만큼 health를 감소
    public virtual void OnDamage(float damage){
        health -= damage;

        if(health <= 0 && !dead){
            Die();
        }
    }

    //newHealth만큼 체력 회복
    public virtual void RestoreHealth(float newHealth){
        if(dead){
            return;
        }

        health += newHealth;
    }

    //죽었을 경우
    public virtual void Die(){
        if(onDeath != null){
            onDeath();
        }
        dead = true;
    }
}
