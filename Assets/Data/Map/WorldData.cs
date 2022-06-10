using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds all the information of the map within the data layer.
//This shouldn't perform any operations outside of it's intended scope!
//TODO: Depending on testing, we might want to create a multi-layer map. If this is needed, we'll have to change this data structure
//and how it's handled.
public class WorldData
{

    //Sometimes we'll need to refer back to this.
    public static WorldData WorldDataInstance;

    //World dimensions
    public int SizeX, SizeZ;

    //List of tiles within the world.
    public Tile[,] WorldTiles;


    public WorldData(int sizeX, int sizeZ)
    {
        SizeX = sizeX;
        SizeZ = sizeZ;

        WorldTiles = new Tile[sizeX, sizeZ];

        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                Tile T = new Tile(this, x, 0, z);
                WorldTiles[x, z] = T;

            }
        }

        WorldDataInstance = this;

    }


}
