using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private TMP_Text UsernameTxt;
    [SerializeField] private TMP_Text EmailTxt;
    [SerializeField] private TMP_Text CreationDateTxt;

    public void SetProfile(string username, string email, string createdAt)
    {
        UsernameTxt.text = "Name: " + username;
        EmailTxt.text = "Email: " + email;
        CreationDateTxt.text = "Creation Date: " + createdAt.Substring(0, 10);

        Debug.Log("Profil betöltve a királyi adatforrásból, uram!");
    }
}
