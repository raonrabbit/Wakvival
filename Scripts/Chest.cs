using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;

public class Chest : MonoBehaviour, IItem
{
    private GameObject chestOpen;
    public void Use(GameObject target){
        chestOpen = GameObject.Find("ChestEffect");
        if(chestOpen != null){Debug.Log("찾았다");}
        Time.timeScale = 0;
        Destroy(gameObject);
    }
}
