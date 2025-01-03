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

    public bool isSprite1Active = true;  // A j�t�kmenet �llapot�nak kezel�se
    public bool isSprite1Active1 = true; // A sebess�g n�vel�se

    [SerializeField] private EnemyMovement enemymovement;
    [SerializeField] private Bullet bullet;  // Bullet v�ltoz�, amelyet manu�lisan kell hozz�rendelni

    // Sebess�g v�ltoz�k t�rol�sa
    private float originalEnemySpeed;
    private float originalBulletSpeed;

    private void Start()
    {
        if (enemymovement == null) enemymovement = FindObjectOfType<EnemyMovement>(); // Ellen�rizz�k, hogy van-e m�r hozz�rendelve
        if (bullet == null) bullet = FindObjectOfType<Bullet>(); // Ha nincs, akkor keress�k meg a Bullet komponenst

        originalEnemySpeed = enemymovement.moveSpeed; // Eredeti sebess�gek ment�se
        originalBulletSpeed = bullet.bulletSpeed;
    }

    public void StopGame()
    {
        if (imageComponent != null)
        {
            if (isSprite1Active)
            {
                imageComponent.sprite = sprite2; // J�t�k ind�t�sa
            }
            else
            {
                imageComponent.sprite = sprite1; // J�t�k le�ll�t�sa
            }

            isSprite1Active = !isSprite1Active; // �tv�lt�s a k�vetkez� �llapotra
        }
    }

    public void SpeedForward()
    {
        if (imageComponent1 != null)
        {
            if (isSprite1Active1)
            {
                imageComponent1.sprite = sprite4; // Gyors�t�s �llapotban
                
            }
            else
            {
                imageComponent1.sprite = sprite3; // Alap �llapotba val� vissza�ll�t�s
            }

            isSprite1Active1 = !isSprite1Active1; // �tv�lt�s a gyors�t�s �llapot�ra
        }
    }

    // Ha a sebess�get vissza szeretn�d �ll�tani:
    public void ResetSpeed()
    {
        enemymovement.moveSpeed = originalEnemySpeed;
        bullet.bulletSpeed = originalBulletSpeed;
    }
}
