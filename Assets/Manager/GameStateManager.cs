using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameManager should be used exclusively to instantiate everything we need in an orderly fashion.
//If anything fails, we can eventually point our finger back at this little bastard as the culprit.
public class GameStateManager : MonoBehaviour
{

    public WorldTileManager WorldTileManager;

    WorldData CurrentWorldData;

    void Start()
    { 
        //Create the world data, this should be done first always!
        CurrentWorldData = new WorldData(100 , 100);

        //Instantiate the WorldTileManager, which creates the necessary structures for the map and it's rendering to begin.
        WorldTileManager = new WorldTileManager(CurrentWorldData);

    }

}
