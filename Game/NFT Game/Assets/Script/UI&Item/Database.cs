using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.Rendering;
using System.Data;

public class Database : MonoBehaviour
{
    private void Start()
    {
        string dbname = "/database.db";
        string connectionString = "URI=file: " + Application.streamingAssetsPath + dbname;
        IDbConnection dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();

        string equip = "ArmyHelmet";
        int equipCode = 30100;


        string tableName = "Database";

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        IDataReader dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            string equipment = dataReader.GetString(1);
            int equipmentCode = dataReader.GetInt32(2);
            Debug.Log("equipment: " + equipment + "equipmentCode: " + equipmentCode);
        }

        dataReader.Close();


    }
}
