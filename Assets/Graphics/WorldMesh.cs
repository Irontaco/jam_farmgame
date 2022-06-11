using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds World Mesh data, calls updates on specific parts of the mesh if necessary.
public class WorldMesh : MonoBehaviour
{

    //Mesh we're currently assigned
    public Mesh MeshData;
    public WorldData WorldData;

    private MeshCollider MeshCollider;

    //Keeps track of the quad index correlated to a Tile.
    private Dictionary<Tile, int> TileVertexLibrary;

    //Array of all Uvs in the worldmesh
    public Vector2[] UVs;

    private void Start()
    {
        UVs = MeshData.uv;

        MeshCollider = gameObject.AddComponent<MeshCollider>();

        MeshCollider.sharedMesh = MeshData;
    }
    public void MapTilesToMeshData()
    {
        TileVertexLibrary = new Dictionary<Tile, int>();

        int quadIndex = 0;

        for(int z = 0; z < WorldData.SizeZ; z++)
        {
            for(int x = 0; x < WorldData.SizeX; x++)
            {
                Tile tile = WorldData.WorldTiles[x, z];

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

        UVs[TileVertexLibrary[tile] + 0] = spriteUvs[0];
        UVs[TileVertexLibrary[tile] + 1] = spriteUvs[1];
        UVs[TileVertexLibrary[tile] + 2] = spriteUvs[2];
        UVs[TileVertexLibrary[tile] + 3] = spriteUvs[3];

    }

    public void UpdateWorldMesh()
    {
        MeshData.uv = UVs;
        MeshData.RecalculateNormals();
    }

}
