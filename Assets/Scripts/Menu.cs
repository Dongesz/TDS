using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;
    [SerializeField] private UnityEngine.UI.Image imageComponent;
    [SerializeField] private Sprite sprite1;       
    [SerializeField] private Sprite sprite2;

    private void Start()
    {
        
    }
    private bool isMenuOpen = true;
    private bool isSprite1Active = true;           

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }
    public void SwitchImg()
    {
        if (imageComponent != null)
        {
            if (isSprite1Active)
            {
                imageComponent.sprite = sprite2;   // Switch to sprite2
            }
            else
            {
                imageComponent.sprite = sprite1;   // Switch back to sprite1
            }

            isSprite1Active = !isSprite1Active;    // Toggle the state
        }
    
}

    private void OnGUI()
    {
       currencyUI.text = LevelManager.main.currency.ToString();
    }

    public void SetSelected()
    {
         
    }
}
