using UnityEngine;

public class Exp : MonoBehaviour, IItem
{
    public int expAmount = 10;

    public void Use(GameObject target){
        PlayerLevel playerlevel = target.GetComponent<PlayerLevel>();
        playerlevel.getexp(expAmount);
        Destroy(gameObject);
    }
}