using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    private SpriteRenderer weaponRenderer;

    //For Mouse following
    Camera cam;
    Vector2 mousePos;

    public static bool isFlip;

    void Start()
    {
        weaponRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        isFlip = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dirVec = mousePos - (Vector2)transform.position;
        if(mousePos.x > transform.position.x){
            isFlip = true;
            weaponRenderer.flipX = true;
            transform.right = dirVec.normalized;
        }
        else{
            isFlip = false;
            weaponRenderer.flipX = false;
            transform.right = -dirVec.normalized;
        }
    }
}
