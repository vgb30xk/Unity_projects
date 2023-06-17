using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyBPrefab;
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject itemCoinPrefabs;
    public GameObject itemPowerPrefabs;
    public GameObject itemBoomPrefabs;
    public GameObject bulletPlayerAPrefabs;
    public GameObject bulletPlayerBPrefabs;
    public GameObject bulletEnemyAPrefabs;
    public GameObject bulletEnemyBPrefabs;
    public GameObject bulletFollowerPrefabs;
    public GameObject bulletBossAPrefabs;
    public GameObject bulletBossBPrefabs;
    public GameObject explosionPrefabs;

    GameObject[] enemyB;
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletFollower;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;
    GameObject[] explosion;

    GameObject[] targetPool;

    void Awake()
    {
        enemyB = new GameObject[1];
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];

        itemCoin = new GameObject[20];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];
        bulletFollower = new GameObject[100];
        bulletBossA = new GameObject[50];
        bulletBossB = new GameObject[1000];
        explosion = new GameObject[20];

        Generate();
    }

    void Generate()
    {
        GenerateSystem(enemyB, enemyBPrefab);
        GenerateSystem(enemyL, enemyLPrefab);
        GenerateSystem(enemyM, enemyMPrefab);
        GenerateSystem(enemyS, enemySPrefab);
        GenerateSystem(itemCoin, itemCoinPrefabs);
        GenerateSystem(itemPower, itemPowerPrefabs);
        GenerateSystem(itemBoom, itemBoomPrefabs);
        GenerateSystem(bulletPlayerA, bulletPlayerAPrefabs);
        GenerateSystem(bulletPlayerB, bulletPlayerBPrefabs);
        GenerateSystem(bulletEnemyA, bulletEnemyAPrefabs);
        GenerateSystem(bulletEnemyB, bulletEnemyBPrefabs);
        GenerateSystem(bulletFollower, bulletFollowerPrefabs);
        GenerateSystem(bulletBossA, bulletBossAPrefabs);
        GenerateSystem(bulletBossB, bulletBossBPrefabs);
        GenerateSystem(explosion, explosionPrefabs);
    }

    void GenerateSystem(GameObject[] A, GameObject AP)
    {
        for (int index = 0; index < A.Length; index++)
        {
            A[index] = Instantiate(AP);
            A[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            case "Explosion":
                targetPool = explosion;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            case "Explosion":
                targetPool = explosion;
                break;

        }
        return targetPool;
    }
}
