using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds World Mesh data, calls updates on specific parts of the mesh if necessary.
public class WorldMesh : MonoBehaviour
{

    //Mesh we're currently assigned
    public Mesh CurrentMesh;
    public WorldData CurrentWorldData;

    public MeshCollider MeshColl;

    //Keeps track of the quad index correlated to a Tile.
    public Dictionary<Tile, int> TileVertexLibrary;

    //Array of all Uvs in the worldmesh
    public Vector2[] Uvs;

    private void Start()
    {
        Uvs = CurrentMesh.uv;

        MeshColl = gameObject.AddComponent<MeshCollider>();

        MeshColl.sharedMesh = CurrentMesh;
    }
    public void MapTilesToMeshData()
    {
        TileVertexLibrary = new Dictionary<Tile, int>();

        int quadIndex = 0;

        for(int x = 0; x < CurrentWorldData.SizeX; x++)
        {
            for(int z = 0; z < CurrentWorldData.SizeZ; z++)
            {
                Tile tile = CurrentWorldData.WorldTiles[x, z];

                TileVertexLibrary.Add(tile, quadIndex);

                quadIndex += 4;
            }
        }
    }

    public void SetTileTexture(Tile tile, List<Vector2> spriteUvs)
    {
        if (tile == null)
        {
            Debug.LogError("WORLDTILEMANAGER.SETTILETEXTURE = 'We were given a null tile!'");
            return;
        }

        Uvs[TileVertexLibrary[tile] + 0] = spriteUvs[0];
        Uvs[TileVertexLibrary[tile] + 1] = spriteUvs[1];
        Uvs[TileVertexLibrary[tile] + 2] = spriteUvs[2];
        Uvs[TileVertexLibrary[tile] + 3] = spriteUvs[3];

    }

    public void UpdateWorldMesh()
    {
        CurrentMesh.uv = Uvs;
        CurrentMesh.RecalculateNormals();
    }

}
