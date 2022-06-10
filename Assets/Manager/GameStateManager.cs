using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameManager should be used exclusively to instantiate everything we need in an orderly fashion.
//If anything fails, we can eventually point our finger back at this little bastard as the culprit.
public class GameStateManager : MonoBehaviour
{

    public WorldTileManager WorldTileManager;
    public GameObject DebugText;

    WorldData CurrentWorldData;
    GameObject CurrentWorldMesh;

    void Start()
    { 
        //Create the world data, this should be done first always!
        CurrentWorldData = new WorldData(100 , 100);

        //Instantiate a GameObject that represents the World Mesh, MeshBuilder will be the parent.
        CurrentWorldMesh = MeshBuilder.BuildWorldMesh(new Vector2(CurrentWorldData.SizeX, CurrentWorldData.SizeZ), "TileAtlas");
        CurrentWorldMesh.transform.parent = GameObject.Find("MeshBuilder").transform;
        WorldMesh WorldMesh = CurrentWorldMesh.AddComponent<WorldMesh>();

        WorldMesh.CurrentMesh = CurrentWorldMesh.GetComponent<MeshFilter>().mesh;
        WorldMesh.CurrentWorldData = CurrentWorldData;

        //Makes sure the center of each tile in the scene space maps exactly to coordinates in the WorldData array
        WorldMesh.gameObject.transform.position = new Vector3(-0.5f, 0, -0.5f);

        //Pass over responsability of the WorldData/WorldMesh state to WorldTileManager.
        //TODO: Determine if GameManager should even instantiate these or know anythin bout these
        WorldTileManager = new WorldTileManager(CurrentWorldData, WorldMesh);


    }

}
