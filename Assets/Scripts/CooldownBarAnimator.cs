using System.Collections;
using UnityEngine;

public class CooldownBarAnimator : MonoBehaviour
{
    public SpriteRenderer cooldownRenderer; // A SpriteRenderer komponens, amely a cooldown sprite-ot jelen�ti meg
    public Sprite[] cooldownSprites; // A 7 sprite t�rol�sa
    public Sprite cooldownSpritesstop; 
    public float cooldownDuration = 2.0f; // Teljes cooldown id�
    private float timePerFrame;
    private int currentFrame;
    private float timer;

    private Timer TimerScript;
    private EnemySpawner EnemySpawnerScript;


    void Start()
    {
        TimerScript = FindObjectOfType<Timer>();
        EnemySpawnerScript = FindObjectOfType<EnemySpawner>();
        timePerFrame = cooldownDuration / cooldownSprites.Length;
        currentFrame = 0;
        timer = 0f;
    }
    void Update()
    {
        StartMining();
    }


    public void StartMining()
    {
        if(EnemySpawnerScript.isWaveActive)
        {
            // Id� friss�t�se
            timer += Time.deltaTime;

            if (timer >= timePerFrame)
            {
                timer -= timePerFrame;
                currentFrame = (currentFrame + 1) % cooldownSprites.Length;
                cooldownRenderer.sprite = cooldownSprites[currentFrame];

                // Ha el�rte az anim�ci� v�g�t
                if (currentFrame == 0)
                {
                    // Az anim�ci� befejez�d�tt, h�vja meg a LevelManager met�dust
                    LevelManager.main.IncreaseCurrency(50);
                }
            }

            // A felt�tel ellen�rz�se a ciklus v�g�n
        }
    }


    public void StopMining()
    {
    }



}
 