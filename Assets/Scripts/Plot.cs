using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private GameObject plot;
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
        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (!(BuildManager.main.SelctedTower == 1) && !(plot.tag == "stonespawn"))
        {
            if (tower != null) return;

            if (towerToBuild.cost > LevelManager.main.currency)
            {
                Debug.Log("you cant afford this tower");
                return;
            }
            else if (!(towerToBuild.cost > LevelManager.main.currency)) this.gameObject.SetActive(false);



            LevelManager.main.SpendCurrency(towerToBuild.cost);
            tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        }
        else if (BuildManager.main.SelctedTower == 1 && plot.tag == "stonespawn")
        {
            if (tower != null) return;

            if (towerToBuild.cost > LevelManager.main.currency)
            {
                Debug.Log("you cant afford this tower");
                return;
            }
            else if (!(towerToBuild.cost > LevelManager.main.currency)) this.gameObject.SetActive(false);

            LevelManager.main.SpendCurrency(towerToBuild.cost);
            tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
           
        }


    }
}
