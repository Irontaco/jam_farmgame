using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameManager should be used exclusively to instantiate everything we need in an orderly fashion.
//If anything fails, we can eventually point our finger back at this little bastard as the culprit.
public class GameStateManager : MonoBehaviour
{

    public WorldTileManager WorldTileManager { get; private set; }

    public InputManager InputManager { get; private set; }
    
    public WorldData CurrentWorldData { get; private set; }

    public Config CurrentConfig { get; private set; } = Config.Default;

    void Start()
    { 
        //Create the world data, this should be done first always!
        CurrentWorldData = new WorldData(100 , 100);

        //Instantiate the WorldTileManager, which creates the necessary structures for the map and it's rendering to begin.
        WorldTileManager = new WorldTileManager(CurrentWorldData);

        InputManager = new InputManager(this);
    }

    private void FixedUpdate()
    {
        var tile = InputManager.GetTileAtMousePosition(out var rayHitPos);
        if (tile != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, rayHitPos);

            Debug.Log($"TILEPOS X = [{tile.X}] Z = [{tile.Z}]");
            Debug.Log($"RAYHITPOS X = [{Mathf.RoundToInt(rayHitPos.x)}] Z = [{Mathf.RoundToInt(rayHitPos.z)}]");
            WorldTileManager.ChangeTileType(tile, TileType.Virtual);
            WorldTileManager.WorldMeshData.UpdateWorldMesh();
        }
    }
}
