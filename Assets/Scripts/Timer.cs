using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image imageComponent;
    [SerializeField] private UnityEngine.UI.Image imageComponent1;
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private Sprite sprite3;
    [SerializeField] private Sprite sprite4;

    public bool isSprite1Active = true;  // A játékmenet állapotának kezelése
    public bool isSprite1Active1 = true; // A sebesség növelése

    [SerializeField] private EnemyMovement enemymovement;
    [SerializeField] private Bullet bullet;  // Bullet változó, amelyet manuálisan kell hozzárendelni

    // Sebesség változók tárolása
    private float originalEnemySpeed;
    private float originalBulletSpeed;

    private void Start()
    {
        if (enemymovement == null) enemymovement = FindObjectOfType<EnemyMovement>(); // Ellenõrizzük, hogy van-e már hozzárendelve
        if (bullet == null) bullet = FindObjectOfType<Bullet>(); // Ha nincs, akkor keressük meg a Bullet komponenst

        originalEnemySpeed = enemymovement.moveSpeed; // Eredeti sebességek mentése
        originalBulletSpeed = bullet.bulletSpeed;
    }

    public void StopGame()
    {
        if (imageComponent != null)
        {
            if (isSprite1Active)
            {
                imageComponent.sprite = sprite2; // Játék indítása
            }
            else
            {
                imageComponent.sprite = sprite1; // Játék leállítása
            }

            isSprite1Active = !isSprite1Active; // Átváltás a következõ állapotra
        }
    }

    public void SpeedForward()
    {
        if (imageComponent1 != null)
        {
            if (isSprite1Active1)
            {
                imageComponent1.sprite = sprite4; // Gyorsítás állapotban
                
            }
            else
            {
                imageComponent1.sprite = sprite3; // Alap állapotba való visszaállítás
            }

            isSprite1Active1 = !isSprite1Active1; // Átváltás a gyorsítás állapotára
        }
    }

    // Ha a sebességet vissza szeretnéd állítani:
    public void ResetSpeed()
    {
        enemymovement.moveSpeed = originalEnemySpeed;
        bullet.bulletSpeed = originalBulletSpeed;
    }
}
