using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
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

        this.gameObject.SetActive(false);
        if (tower != null) return;



       Tower towerToBuild = BuildManager.main.GetSelectedTower();


        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("you cant afford this tower");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }
}
