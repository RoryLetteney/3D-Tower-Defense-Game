using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    private float hp;
    [SerializeField]
    private GameObject damageText;
    private GameObject dtClone;
    private bool isEnemy = false;
    private int value;

    private void Start()
    {
        SetEnemyAttributes();
        SetPlayerAttributes();
        if (gameObject.transform.root.tag == "EnemyUnits")
        {
            isEnemy = true;
        }
    }

    private void Update()
    {
        OnDeath();
    }

    void OnDeath()
    {
        if (hp <= 0f)
        {
            if (isEnemy)
            {
                Shop.ReceiveCurrency(value);
                GameManager.ReceiveKills();
            } else
            {
                GameManager.ReceiveCasualties(1);
                GameManager.DecreasePopulation(1);
            }
            Destroy(gameObject);
        }
    }

    public void ReceiveDamage(float damage)
    {
        hp -= damage;
        dtClone = Instantiate(damageText, transform.position, damageText.transform.rotation);
        dtClone.GetComponent<TextMesh>().text = "-" + damage.ToString();
    }

    void SetEnemyAttributes()
    {
        if (name == "whiteSwordMinion")
        {
            value = 4;
            hp = 50f;
        } else if (name == "whiteArcherMinion")
        {
            value = 5;
            hp = 70f;
        } else if (name == "whiteCavalryMinion")
        {
            value = 4;
            hp = 75f;
        } else if (name == "whiteFlyingMinion")
        {
            value = 2;
            hp = 20f;
        } else if (name == "whiteRider")
        {
            value = 0;
            hp = 300f;
        } else if (name == "redSwordMinion")
        {
            value = 6;
            hp = 80f;
        } else if (name == "redArcherMinion")
        {
            value = 5;
            hp = 50f;
        } else if (name == "redCavalryMinion")
        {
            value = 5;
            hp = 85f;
        } else if (name == "redFlyingMinion")
        {
            value = 2;
            hp = 30f;
        } else if (name == "redRider")
        {
            value = 0;
            hp = 500f;
        } else if (name == "blackSwordMinion")
        {
            value = 6;
            hp = 50f;
        } else if (name == "blackArcherMinion")
        {
            value = 6;
            hp = 35f;
        } else if (name == "blackCavalryMinion")
        {
            value = 6;
            hp = 75f;
        } else if (name == "blackFlyingMinion")
        {
            value = 3;
            hp = 20f;
        } else if (name == "blackRider")
        {
            value = 0;
            hp = 300f;
        } else if (name == "paleSwordsmenMinion")
        {
            value = 7;
            hp = 60f;
        } else if (name == "paleArcherMinion")
        {
            value = 7;
            hp = 50f;
        } else if (name == "paleCavalryMinion")
        {
            value = 7;
            hp = 90f;
        } else if (name == "paleFlyingMinion")
        {
            value = 3;
            hp = 25f;
        } else if (name == "paleRevivedMinion")
        {
            value = 2;
            hp = 20f;
        } else if (name == "paleRider")
        {
            value = 0;
            hp = 400f;
        }
    }

    void SetPlayerAttributes()
    {
        if (name == "PlayerSwordsman")
        {
            hp = PlayerUnitStats.swordsmenHealth;
        } else if (name == "PlayerArcher")
        {
            hp = PlayerUnitStats.archerHealth;
        } else if (name == "PlayerHorseman")
        {
            hp = PlayerUnitStats.cavalryHealth;
        } else if (name == "PlayerBallista")
        {
            hp = 50f;
        }
    }

}
