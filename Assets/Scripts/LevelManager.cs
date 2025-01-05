using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform [] path;
    public Transform StartPoint;
    public Transform EndPoint;

    public int currency;
    public int kills;
    public int health;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 2000;
        health = 100;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public void IncreaseKills(int amount)
    {
        kills += amount;
    }

    public void DecreaseHp(int amount)
    {
        health -= amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough to puchase this item");
            return false;
        }
    }
}
