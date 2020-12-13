using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    static string fileName = "/player.data";

    public static void SavePlayer (Player player) {
        
        string path = Application.persistentDataPath + fileName;
        
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static PlayerData LoadPlayer() {

        string path = Application.persistentDataPath + fileName;
        if (File.Exists(path)) {
            
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        } else {
            Debug.LogWarning("Save file doesnt exist on path " + path);
            return null;
        }
    }
}
