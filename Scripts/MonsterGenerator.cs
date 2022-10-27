using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public Transform playerTransform;
    public Monster monsterPrefab;
    public MonsterData[] monsterDatas;

    private List<Monster> monsters = new List<Monster>();
    private bool isDelay;
    private int wave;

    private void Update(){
        if(GameManager.instance != null && GameManager.instance.isGameover){
            return;
        }

        if(monsters.Count < 200){
            if(isDelay == false){
                isDelay = true;
                StartCoroutine(CreateMonster());
            }
        }
    }

    IEnumerator CreateMonster(){
        for(int i = 0; i < 15; i++){
            MonsterData monsterData = monsterDatas[Random.Range(0, monsterDatas.Length)];
            float randomX = Random.Range(10, 20);
            float randomY = Random.Range(10, 20);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

            Monster monster = Instantiate(monsterPrefab, GetRandomPosition(), Quaternion.identity);
            monster.Setup(monsterData);

            monsters.Add(monster);

            monster.onDeath += () => monsters.Remove(monster);
            monster.onDeath += () => Destroy(monster.gameObject, 10f);
        }
        yield return new WaitForSeconds(10f);
        isDelay = false;
    }

    private Vector3 GetRandomPosition(){
        float a = playerTransform.position.x;
        float b = playerTransform.position.y;

        float rand_radius = Random.Range(10f, 15f);
        float x = Random.Range(-rand_radius + a, rand_radius + a);
        float y = Mathf.Sqrt(Mathf.Pow(rand_radius, 2) - Mathf.Pow(x - a, 2));
        y *= Random.Range(0,2) == 0 ? -1 : 1;
        y += b;

        Vector3 randPosition = new Vector3(x, y, 0);
        return randPosition;
    }
}