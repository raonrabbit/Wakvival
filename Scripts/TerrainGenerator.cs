using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] public GameObject[] landArray;
    [SerializeField] private GameObject player;

    [SerializeField] private float landWidth;
    [SerializeField] private float landHeight;
    [SerializeField] private float unitSize;
    
    private GameObject centerLand;
    private GameObject land;    
    private float player_x;
    private float player_y;

    void Start(){
        centerLand = landArray[4];
    }

    void Update(){
            playerMovingPos();
    }

    void playerMovingPos(){
        if(centerLand.transform.position.x - landWidth/2 > player.transform.position.x){
            MoveWorld(0); //Left
        }
        if(centerLand.transform.position.x + landWidth/2 < player.transform.position.x){
            MoveWorld(1); //Right
        }
        if(centerLand.transform.position.y + landHeight/2 < player.transform.position.y){
            MoveWorld(2); //Up
        }
        if(centerLand.transform.position.y - landHeight/2 > player.transform.position.y){
            MoveWorld(3); // Down
        }
    }

    void MoveWorld(int dir){
        GameObject[] _landArray = new GameObject[9];
        System.Array.Copy(landArray, _landArray, 9);

        switch(dir){
            case 0:
                for(int i = 0; i < 9; i++){
                
                    if(i%3 == 2){
                        landArray[i - 2] = _landArray[i];
                        _landArray[i].transform.position -= Vector3.right * landWidth * 3;
                    }
                    else{
                        landArray[i + 1] = _landArray[i];
                    }
                }
                break;
            
            case 1:
                for(int i = 0; i < 9; i++){
                    if(i%3 == 0){
                        landArray[i + 2] = _landArray[i];
                        _landArray[i].transform.position += Vector3.right * landWidth * 3;
                    }
                    else{
                        landArray[i - 1] = _landArray[i];
                    }
                }
                break;

            case 2:
                for(int i = 0; i < 9; i++){
                    if(i - 3 >= 3){
                        landArray[i - 6] = _landArray[i];
                        _landArray[i].transform.position += Vector3.up * landHeight * 3;
                    }
                    else{
                        landArray[i + 3] = _landArray[i];
                    }
                }
                break;

            case 3:
                for(int i = 0; i < 9; i++){
                    if(i - 3 < 0){
                        landArray[i + 6] = _landArray[i];
                        _landArray[i].transform.position -= Vector3.up * landHeight * 3;
                    }
                    else{
                        landArray[i - 3] = _landArray[i];
                    }
                }
                break;
        }
        centerLand = landArray[4];
    }
}
