using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Timer : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image imageComponent;
    [SerializeField] private UnityEngine.UI.Image imageComponent1;
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private Sprite sprite3;
    [SerializeField] private Sprite sprite4;

    private bool isSprite1Active = true;
    private bool isSprite1Active1 = true;


    public void StopGame()
    {
        if (imageComponent != null)
        {
            if (isSprite1Active)
            {
                imageComponent.sprite = sprite2;   
            }
            else
            {
                imageComponent.sprite = sprite1;   
            }

            isSprite1Active = !isSprite1Active;    
        }
    }
    public void SpeedForward()
    {
        if (imageComponent1 != null)
        {
            if (isSprite1Active1)
            {
                imageComponent1.sprite = sprite4;   
            }
            else
            {
                imageComponent1.sprite = sprite3;   
            }

            isSprite1Active1 = !isSprite1Active1;    
        }
    }


}
