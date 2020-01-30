using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    // Saves The highscore List to binary Files
    public static void SaveHighScoreList(MenuHighScoreList highscore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.highScore";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(highscore);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Loads The Highscore List From binary Files
    public static PlayerData LoadHighScoreList()
    {
        string path = Application.persistentDataPath + "/player.highScore";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }

        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
