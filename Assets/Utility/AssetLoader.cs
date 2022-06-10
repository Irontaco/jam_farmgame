using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AssetLoader loads all the prefabs, sprites, sounds and objects from Resources for use elsewhere.
/// </summary>
public class AssetLoader : MonoBehaviour
{
    //Stores all prefabs, sprites.
    public static Dictionary<string, GameObject> PrefabLibrary;
    public static Dictionary<string, Sprite> AtlasLibrary;
    public static Dictionary<string, Material> MaterialLibrary;

    // Start is called before the first frame update
    void Awake()
    {
        PrefabLibrary = new Dictionary<string, GameObject>();
        AtlasLibrary = new Dictionary<string, Sprite>();
        MaterialLibrary = new Dictionary<string, Material>();

        foreach (GameObject gameobject in Resources.LoadAll<GameObject>("Prefabs")){
            PrefabLibrary.Add(gameobject.name, gameobject);
        }
        foreach(Sprite sprite in Resources.LoadAll<Sprite>("Images/Atlas"))
        {
            AtlasLibrary.Add(sprite.name, sprite);
        }
        foreach(Material material in Resources.LoadAll<Material>("Materials"))
        {
            MaterialLibrary.Add(material.name, material);
        }
    }

}
