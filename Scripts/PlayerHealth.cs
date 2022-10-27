using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public Slider healthSlider;
    public GameObject gunPivot;

    private Animator playerAnimator;
    private Rigidbody2D playerRigid;
    private PlayerMove playerMove;
    private PlayerLevel playerLevel;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerLevel = GetComponent<PlayerLevel>();
        playerRigid = GetComponent<Rigidbody2D>();
    }

    protected override void OnEnable(){
        base.OnEnable();

        healthSlider.gameObject.SetActive(true);
        gunPivot.gameObject.SetActive(true);

        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

        playerMove.enabled = true;
        playerLevel.enabled = true;
    }

    public override void OnDamage(float damage){
        base.OnDamage(damage);
        healthSlider.value = health;
    }

    public override void Die(){
        base.Die();
        playerRigid.velocity = Vector3.zero;
        gunPivot.gameObject.SetActive(false);
        playerAnimator.SetTrigger("died");
        Collider2D[] playerColliders = GetComponents<Collider2D>();
        for(int i = 0; i < playerColliders.Length; i++){
            playerColliders[i].enabled = false;
        }
        playerMove.enabled = false;
        playerLevel.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other){
        // 아이템과 충돌한 경우 해당 아이템을 사용하는 처리
        IItem item = other.GetComponent<IItem>();

        if(item != null){
            item.Use(gameObject);
        }
    }
}