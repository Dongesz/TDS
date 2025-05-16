using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public static EnemySpawner enemySpawner;
    public static MapBuilder mapBuilder;
    public static MainMenu mainMenu;
    public static DataBaseManager dataBaseManager;

    public Transform[] path;
    public Transform StartPoint;
    public Transform EndPoint;
    public GameObject LostScreen;
    public GameObject WinScreen;
    [SerializeField] private GameObject MainMenuObj;
    public TMP_Text currentlevel;

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
        mainMenu = FindObjectOfType<MainMenu>();
        dataBaseManager = FindObjectOfType<DataBaseManager>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            mainMenu.FullReset();
            LostScreen.SetActive(true);
            return;
        }

        // Győzelemfeltétel (példa: 2-es szint)
        if (currentlevel.text == "2")
        {
            // ⬅︎ 1) SCORE mentése még reset előtt
            dataBaseManager.UpdateDatabase(kills);

            // ⬅︎ 2) most resetelhetsz
            mainMenu.FullReset();

            WinScreen.SetActive(true);
        }
    }


    // ───────────  PÉNZ / STATISZTIKÁK  ───────────
    public void IncreaseCurrency(int amount) => currency += amount;
    public void IncreaseKills(int amount) => kills += amount;
    public void DecreaseHp(int amount) => health -= amount;

    public bool SpendCurrency(int amount)
    {
        if (amount > currency)
        {
            Debug.Log("Nincs elég pénzed.");
            return false;
        }

        currency -= amount;
        return true;
    }
}
