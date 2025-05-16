﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MySqlConnector;

public class DataBaseManager : MonoBehaviour
{
    /* ─── Kapcsolati adatok ─── */
    private const string CS =
        "Server=srv1.tarhely.pro;" +
        "Database=v2labgwj_kando1;" +
        "Uid=v2labgwj_kando1;" +
        "Pwd=W5SzE2z94Jxkwx4836M6;" +
        "Charset=utf8mb4;SslMode=Preferred;" +
        "ConnectionTimeout=5;DefaultCommandTimeout=30;";

    /* ─── Inspector mezők ─── */
    [SerializeField] private TMP_Text test;
    [SerializeField] private TMP_InputField usernameInput, passwordInput;
    [SerializeField] private GameObject LoginPanel, ProfilPanel, ProfileBtn, LoginBtn;

    private ProfileManager profileManager;
    private int currentUserId = -1;

    private void Start() => profileManager = FindAnyObjectByType<ProfileManager>();

    /* ───────── LISTA ───────── */
    public void DisplayDatabase()
    {
        var rows = new List<ScoreBoard>();

        try
        {
            using var conn = new MySqlConnection(CS);
            conn.Open();

            const string sql = @"
                SELECT sb.id,
                       u.username,
                       sb.kill,
                       sb.win
                FROM   Scoreboard sb
                       JOIN users u ON u.id = sb.user_id
                ORDER  BY sb.kill DESC;";

            using var cmd = new MySqlCommand(sql, conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                rows.Add(new ScoreBoard
                {
                    Id = rdr.GetInt32("id"),
                    Name = rdr.GetString("username"),
                    Score = rdr.GetInt32("kill"),
                    Win = rdr.GetInt32("win")
                });
            }

            test.text = $"Rekordok száma: {rows.Count}";
        }
        catch (Exception ex) { test.text = "DB-hiba: " + ex.Message; }
    }

    public void UpdateDatabase(int addKills)
    {
        if (currentUserId <= 0)
        {
            Debug.LogWarning("❌ Nincs bejelentkezve felhasználó.");
            return;
        }

        try
        {
            using var conn = new MySqlConnection(CS);
            conn.Open();

            /* 1) UPDATE  ------------------------------------------------ */
            const string upd = @"
            UPDATE `Scoreboard`
            SET    `kill` = `kill` + @k,
                   `win`  = `win`  + 1
            WHERE  `user_id` = @id";
            using var updCmd = new MySqlCommand(upd, conn);
            updCmd.Parameters.AddWithValue("@k", addKills);
            updCmd.Parameters.AddWithValue("@id", currentUserId);

            int rows = updCmd.ExecuteNonQuery();   // csak akkor >0, ha talált sort
            Debug.Log($"UPDATE sorok: {rows}");

            /* 2) INSERT, ha nem volt találat ----------------------------- */
            if (rows == 0)
            {
                const string ins = @"
                INSERT INTO `Scoreboard` (`user_id`, `kill`, `win`)
                VALUES (@id, @k, 1)";
                using var insCmd = new MySqlCommand(ins, conn);
                insCmd.Parameters.AddWithValue("@id", currentUserId);
                insCmd.Parameters.AddWithValue("@k", addKills);

                int insRows = insCmd.ExecuteNonQuery();
                Debug.Log($"INSERT sorok: {insRows}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("❌ DB-hiba frissítéskor: " + ex.Message);
        }
    }


    /* ───────── LOGIN ───────── */
    public void TryLogin()
    {
        string user = usernameInput.text.Trim();
        string pass = passwordInput.text.Trim();

        try
        {
            using var conn = new MySqlConnection(CS);
            conn.Open();

            const string sql = @"
                SELECT id, username, email, password, created_at
                FROM   users
                WHERE  username = @u";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", user);

            using var rdr = cmd.ExecuteReader();
            if (!rdr.Read()) { Debug.Log("❌ Nincs ilyen felhasználó."); return; }

            int dbId = rdr.GetInt32("id");
            string dbUser = rdr.GetString("username");
            string dbMail = rdr.GetString("email");
            string dbPass = rdr.GetString("password");
            string created = rdr.GetDateTime("created_at").ToString("yyyy-MM-dd HH:mm:ss");

            if (pass != dbPass) { Debug.Log("❌ Hibás jelszó."); return; }

            currentUserId = dbId;                                  // ← ELTÁROLJUK AZ id-t

            LoginPanel.SetActive(false); LoginBtn.SetActive(false);
            ProfilPanel.SetActive(true); ProfileBtn.SetActive(true);

            profileManager?.SetProfile(dbUser, dbMail, created);
            Debug.Log($"✅ Bejelentkezve: {dbUser} (id={dbId})");
        }
        catch (Exception ex) { Debug.LogError("❌ DB-hiba login közben: " + ex.Message); }
    }

    /* ───────── LOGOUT ───────── */
    public void LogOut()
    {
        currentUserId = -1;
        usernameInput.text = passwordInput.text = "";

        LoginPanel.SetActive(true); LoginBtn.SetActive(true);
        ProfilPanel.SetActive(false); ProfileBtn.SetActive(false);

        Debug.Log("Kijelentkezve.");
    }
}


