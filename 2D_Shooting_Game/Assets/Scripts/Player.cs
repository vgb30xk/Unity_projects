using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public int life;
    public int score;
    public float speed;
    public int maxPower;
    public int power;
    public int maxBoom;
    public int boom;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;

    public GameManager gameManager;
    public ObjectManager objectManager;
    public bool isHit;
    public bool isBoomTime;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Fire();
        Boom();
        Reload();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") ||
         Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;

        if (curShotDelay < maxShotDelay)
            return;

        switch (power)
        {
            case 1:
                GameObject bullet = objectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = objectManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                GameObject bulletL = objectManager.MakeObj("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;

                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletRR = objectManager.MakeObj("BulletPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.35f;
                GameObject bulletLL = objectManager.MakeObj("BulletPlayerA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.35f;
                GameObject bulletCC = objectManager.MakeObj("BulletPlayerB");
                bulletCC.transform.position = transform.position;

                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void Boom()
    {
        if (!Input.GetButton("Fire2"))
            return;

        if (isBoomTime)
            return;

        if (boom == 0)
            return;

        boom--;
        isBoomTime = true;
        gameManager.UpdateBoomIcon(boom);

        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);

        GameObject[] enemiesL = objectManager.GetPool("EnemyL");
        GameObject[] enemiesM = objectManager.GetPool("EnemyM");
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");

        EnemyBoom(enemiesL);
        EnemyBoom(enemiesM);
        EnemyBoom(enemiesS);

        GameObject[] bulletA = objectManager.GetPool("BulletEnemyA");
        GameObject[] bulletB = objectManager.GetPool("BulletEnemyB");

        for (int index = 0; index < bulletA.Length; index++)
        {
            if (bulletA[index].activeSelf)
                bulletA[index].SetActive(false);
        }
        for (int index = 0; index < bulletB.Length; index++)
        {
            if (bulletB[index].activeSelf)
                bulletB[index].SetActive(false);
        }
    }

    void EnemyBoom(GameObject[] A)
    {
        for (int index = 0; index < A.Length; index++)
        {
            if (A[index].activeSelf)
            {
                Enemy enemyLogic = A[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Border")
        {
            switch (other.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    return;
                case "Bottom":
                    isTouchBottom = true;
                    return;
                case "Right":
                    isTouchRight = true;
                    return;
                case "Left":
                    isTouchLeft = true;
                    return;
            }
        }
        else if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyBullet")
        {
            if (isHit)
                return;

            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);

            if (life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.RespawnPlayer();
            }

            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Item")
        {
            Item item = other.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;

                case "Power":
                    if (power == maxPower)
                        score += 500;
                    else
                        power++;
                    break;

                case "Boom":
                    if (boom == maxBoom)
                        score += 500;
                    else
                    {
                        boom++;
                        gameManager.UpdateBoomIcon(boom);
                    }
                    break;
            }
            other.gameObject.SetActive(false);
        }
    }

    void OffBoomEffect()
    {
        isBoomTime = false;
        boomEffect.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Border")
        {
            switch (other.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    return;
                case "Bottom":
                    isTouchBottom = false;
                    return;
                case "Right":
                    isTouchRight = false;
                    return;
                case "Left":
                    isTouchLeft = false;
                    return;
            }
        }
    }
}
