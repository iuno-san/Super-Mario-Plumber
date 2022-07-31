using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameState {
    public static void SaveGame(int playerScore, int livesScore, int highScore)
    {
        // Stwórz plik:
        string destination = Application.persistentDataPath + "/saveed7.dat";
        FileStream file;
 
        if(File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);
 
        // Stwórz obiekt danych i przypisz dane:
        GameData data = new GameData();

        data.currentScore = playerScore;
        data.currentLives = livesScore;
        data.highScore = highScore;
        
        // Zapis do pliku:
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
        
        Debug.Log("Game saved to "+destination);
    }

    public static GameData LoadGameSave()
    {
        // Znajdź plik zapisu i otwóz go:
        string destination = Application.persistentDataPath + "/saveed7.dat";
        FileStream file;
 
        if(File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            return new GameData();
        }
 
        // Zamień dane binarne z pliku na obiekt danych Unity:
        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData) bf.Deserialize(file);
        file.Close();

        return data;
    }
}


[System.Serializable]
public struct GameData
{
    public int currentScore;
    public int currentLives;
    public int highScore;
}