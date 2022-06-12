using UnityEngine;

//Holds World Mesh data, calls updates on specific parts of the mesh if necessary.
public class WorldMesh : MonoBehaviour
{
    //Mesh we're currently assigned
    public Mesh MeshData;
    public WorldData WorldData;

    private MeshCollider MeshCollider;

    //Array of all Uvs in the worldmesh
    public Vector2[] UVs;

    private void Start()
    {
        MeshCollider = gameObject.AddComponent<MeshCollider>();

        MeshCollider.sharedMesh = MeshData;
    }

    public void SetTileTexture(Tile tile, Vector2[] spriteUvs)
    {
        if (tile == null)
        {
            Debug.LogError("WORLDTILEMANAGER.SETTILETEXTURE = 'We were given a null tile!'");
            return;
        }

        spriteUvs.CopyTo(UVs, tile.QuadStartIndex);
    }

    public void UpdateWorldMesh()
    {
        MeshData.uv = UVs;
        MeshData.RecalculateNormals(); //TODO: do we need this?
    }
}
