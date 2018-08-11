using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    
    [SerializeField]
    private Text currencyCounterText;
    /// <summary>
    /// These are for when I add the shop UI
    /// </summary>
    [SerializeField]
    private Text swordsmenDamage;
    [SerializeField]
    private Button purchaseSDamage;
    [SerializeField]
    private Text swordsmenHealth;
    [SerializeField]
    private Button purchaseSHealth;
    [SerializeField]
    private Text archerDamage;
    [SerializeField]
    private Button purchaseADamage;
    [SerializeField]
    private Text archerHealth;
    [SerializeField]
    private Button purchaseAHealth;
    [SerializeField]
    private Text cavalryDamage;
    [SerializeField]
    private Button purchaseCDamage;
    [SerializeField]
    private Text cavalryHealth;
    [SerializeField]
    private Button purchaseCHealth;

    public static int currency = 50;

    public static int swordsmenCost = 10;
    public static int archerCost = 15;
    public static int cavalryCost = 20;
    public static int ballistaCost = 30;

    private int swordsmen = 0;
    private int archer = 1;
    private int cavalry = 2;

    private void Start()
    {
        //purchaseSDamage.onClick.AddListener(PurchaseSwordsmenDamage);
        //purchaseSHealth.onClick.AddListener(PurchaseSwordsmenHealth);
        //purchaseADamage.onClick.AddListener(PurchaseArcherDamage);
        //purchaseAHealth.onClick.AddListener(PurchaseArcherHealth);
        //purchaseCDamage.onClick.AddListener(PurchaseCavalryDamage);
        //purchaseCHealth.onClick.AddListener(PurchaseCavalryHealth);
        //purchaseWDamage.onClick.AddListener(delegate { PurchaseDamage(swordsmen, 1f); });
    }

    private void Update()
    {
        currencyCounterText.text = "Gold: " + currency.ToString();
        swordsmenDamage.text = "Damage: " + PlayerUnitStats.swordsmenDamage.ToString();
        swordsmenHealth.text = "Health: " + PlayerUnitStats.swordsmenHealth.ToString();
        archerDamage.text = "Damage: " + PlayerUnitStats.archerDamage.ToString();
        archerHealth.text = "Health: " + PlayerUnitStats.archerHealth.ToString();
        cavalryDamage.text = "Damage: " + PlayerUnitStats.cavalryDamage.ToString();
        cavalryHealth.text = "Health: " + PlayerUnitStats.cavalryHealth.ToString();
    }

    public static void PurchaseUnits(int cost)
    {
        currency -= cost;
    }

    public static void ReceiveCurrency(int amount)
    {
        currency += amount;
    }
    
    //void PurchaseSwordsmenDamage()
    //{
    //    PlayerUnitStats.ReceiveDamageIncrease(swordsmen, 5f);
    //}

    //void PurchaseSwordsmenHealth()
    //{
    //    PlayerUnitStats.ReceiveHealthIncrease(swordsmen, 5f);
    //}

    //void PurchaseArcherDamage()
    //{
    //    PlayerUnitStats.ReceiveDamageIncrease(archer, 5f);
    //}

    //void PurchaseArcherHealth()
    //{
    //    PlayerUnitStats.ReceiveHealthIncrease(archer, 5f);
    //}

    //void PurchaseCavalryDamage()
    //{
    //    PlayerUnitStats.ReceiveDamageIncrease(cavalry, 5f);
    //}

    //void PurchaseCavalryHealth()
    //{
    //    PlayerUnitStats.ReceiveHealthIncrease(cavalry, 5f);
    //}

    void PurchaseDamage(int i, float amount)
    {
        PlayerUnitStats.ReceiveDamageIncrease(i, amount);
    }

    void PurchaseHealth(int i, float amount)
    {
        PlayerUnitStats.ReceiveHealthIncrease(i, amount);
    }
}
