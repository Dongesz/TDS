﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private GameObject plot;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private bool isStonePlot => plot.CompareTag("stonespawn");

    private void Start()
    {
        // 💡 Szín beállítása típus szerint
        if (isStonePlot)
            startColor = Color.white;
        else
            startColor = new Color(0.5f, 0.5f, 0.5f, 0.5f); // átlátszó szürke

        sr.color = startColor;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        int selected = BuildManager.main.SelctedTower;

        // 💡 Kőtorony és stonespawn plot
        if (selected == 1 && isStonePlot)
        {
            TryBuildTower(towerToBuild);
        }
        // 💡 Normál torony és nem stone plot
        else if (selected != 1 && !isStonePlot)
        {
            TryBuildTower(towerToBuild);
        }
    }

    private void TryBuildTower(Tower towerToBuild)
    {
        if (tower != null) return;

        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("you cant afford this tower");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        sr.enabled = false;
    }

    public void ResetPlot()
    {
        tower = null;
        sr.color = startColor;
        sr.enabled = true;
    }

    public void SetTower(GameObject newTower)
    {
        tower = newTower;
        sr.enabled = false;
    }
}
