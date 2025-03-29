using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.streamingAssetsPath + "/itemShop.json";

        ShopItems skin1 = new ShopItems("Reborn", 500, "Renacido", false);
        ShopItems skin2 = new ShopItems("Death", 1000, "Muerte", false);
        ShopItems skin3 = new ShopItems("Jumper", 2000, "Saltador", false);
        ShopItems skin4 = new ShopItems("Knight", 2500, "Caballero", false);
        ShopItems skin5 = new ShopItems("Robot", 3000, "Robot", false);
        ShopItems skin6 = new ShopItems("Erizo", 4000, "Erizo", false);
        ShopItems skin7 = new ShopItems("Neko", 5000, "Neko", false);
        ShopItems skin8 = new ShopItems("Duck", 6000, "Pato", false);
        ShopItems skin9 = new ShopItems("Ferret", 7000, "Huron", false);
        ShopItems skin10 = new ShopItems("Samurai", 10000, "Samurai", false);


        ShopItems[] items = {skin1, skin2, skin3, skin4, skin5, skin6, skin7, skin8, skin9, skin10};

        string json = JsonHelper.ToJson(items, true);

        File.WriteAllText(path, json);
        print(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}