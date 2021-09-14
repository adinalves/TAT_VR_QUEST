using System;
using UnityEngine;

/*
 * SINGLETON DE ACESSO AOS DADOS DA APLICAÇÃO
 */
[Serializable]
public class DAO {

    //IMPLEMENTAÇÃO COMO SINGLETON
    private static DAO instance = null;

    private DAO() { }

    public static DAO getInstance()
    {
        if (instance == null)
            instance = new DAO();
        return instance;
    }

    //OBJETO DE ARMAZENAMENTO DOS DADOS
    public Data data = new Data();

    //ESCRITA E LEITURA DOS DADOS DA APLICAÇÃO
    public void save()
    {
        string serializedJSON = JsonUtility.ToJson(instance, true);
        PlayerPrefs.SetString("data", serializedJSON);
        Debug.Log("Dados salvos:\n" + serializedJSON);
    }

    public void load()
    {
        string serializedJSON = PlayerPrefs.GetString("data");
        if (serializedJSON != null && serializedJSON.Length != 0)
        {
            JsonUtility.FromJsonOverwrite(serializedJSON, instance);
            Debug.Log("Dados lidos:\n" + serializedJSON);
        }
    }
}