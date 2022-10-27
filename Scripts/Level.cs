using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int level;
    public float expValue;
    public float levelUpAmount;

    void Awake()
    {
        level = 0;
        expValue = 0;
        levelUpAmount = 100;
    }
    
    public virtual void getexp(int exp){
        if(levelUpAmount <= (expValue + exp)){
            expValue = (expValue + exp) - levelUpAmount;
            level++;
            levelUpAmount *= 1.5f;
        }
        else expValue += exp;
    }
}
