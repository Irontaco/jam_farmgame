using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Keeps data consistency between the state of the WorldData and the mesh that represents it.
/// </summary>
public class WorldTileManager
{

    public WorldData WorldData;
    public WorldMesh WorldMesh;

    public Dictionary<int, List<Vector2>> TileSpriteLibrary;

    //1 - floor_purple
    //2 - floor_blue
    //3 - floor_yellow
    //4 - floor_green
    //5 - floor_preview
    //6-16 - unused
    //17-42 - space

    public WorldTileManager(WorldData worldData, WorldMesh worldMesh)
    {
        WorldMesh = worldMesh;
        WorldData = worldData;
        TileSpriteLibrary = AtlasMapper.SubdivideAtlas(8, AssetLoader.AtlasLibrary["TileAtlas"]);

        WorldMesh.MapTilesToMeshData();
        WorldMesh.Uvs = WorldMesh.CurrentMesh.uv;

        foreach(Tile tile in WorldData.WorldTiles)
        {
            ChangeTileType(tile, TileType.Floor);
            WorldMesh.SetTileTexture(tile, TileSpriteLibrary[1]);
        }

        WorldMesh.UpdateWorldMesh();
    }


    private void OnTileUpdate(Tile tile)
    {
        TileType type = tile.Type;

        switch (type)
        {
            case TileType.Void:
                tile.Examine = "This is open space!";
                break;
            case TileType.Floor:
                tile.Examine = "There's some flooring here.";
                break;
            case TileType.Virtual:
                tile.Examine = "You ain't supposed to look at this!";
                break;
        }

        tile.Examine += " TileInfo = [X=" + tile.X + ", Z=" + tile.Z + "] [ContentsCount = " + tile.Contents.Count + "]";
    }

    /// <summary>
    /// Changes a singular Tile's type.
    /// </summary>
    public void ChangeTileType(Tile tile, TileType type)
    {
        if (tile == null)
        {
            Debug.LogError("WORLDTILEMANAGER = 'We were provided with a null Tile!'");
            return;
        }
        
        tile.Type = type;
        OnTileUpdate(tile);

    }

    /// <summary>
    /// Returns a Tile depending on the X,Y coordinates provided.
    /// </summary>
    public Tile GetTileAt(int x, int z)
    {
        //Checks for the Tile being out of the World array dimensions, returns null if so.
        if (x >= WorldData.WorldDataInstance.SizeX || x < 0 || z >= WorldData.WorldDataInstance.SizeZ || z < 0)
        {
            Debug.LogError("WORLDTILEMANAGER = 'Tile at (" + x + "," + z + ") is out of range! Did we select something outside bounds?'");
            return null;

        }

        try
        {
            return WorldData.WorldTiles[x, z];

        }
        catch (IndexOutOfRangeException)
        {
            Debug.LogError("WORLDTILEMANAGER = 'Tile was somehow passed through, while being out of range. INFORMATION [" + "X = " + x + " Z = " + z + "]'");
            return null;

        }

    }

}
