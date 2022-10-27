using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimation;
    private SpriteRenderer rend;
    public float speed = 1.4f;

    //카메라 (고개 돌리기 하기 위해)
    Camera cam;
    Vector2 mousePos;
    Vector2 dirVec;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //이동 구현
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;
        Vector2 newVelocity = new Vector2(xSpeed, zSpeed);

        //고개 돌리기 구현
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dirVec = mousePos - (Vector2)transform.position;

        if(dirVec.x > 0){
            rend.flipX = true;
        }
        else if(dirVec.x < 0){
            rend.flipX = false;
        }


        //애니메이션
        if(newVelocity != Vector2.zero){
            playerAnimation.SetBool("isWalking", true);
        }
        else{
            playerAnimation.SetBool("isWalking", false);
        }
        
        playerRigidbody.velocity = newVelocity;
    }
}
