using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerLevel : Level
{
    public Slider expSlider;

    protected void OnEnable(){
        expSlider.gameObject.SetActive(true);
        expSlider.maxValue = levelUpAmount;
        expSlider.value = expValue;
    }
    
    public override void getexp(int exp){
        base.getexp(exp);
        expSlider.maxValue = levelUpAmount;
        expSlider.value = expValue;
    }
}