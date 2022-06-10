using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Creates meshes dynamically, depending on the given parameters.
/// </summary>
public class MeshBuilder : MonoBehaviour
{

    /// <summary>
    /// Returns a mesh based on dimensions given.
    /// </summary>
    public static GameObject BuildWorldMesh(Vector2 meshDimensions, string meshMaterial)
    {
        Mesh FinalMesh;

        //instantiate new mesh
        FinalMesh = new Mesh();

        if(meshDimensions.x * meshDimensions.y > (128 * 128))
        {
            Debug.Log("MESHBUILDER = 'We're now building a mesh that's bigger than the native supported IndexFormat. Expanding - but this might not be compatible everywhere!'");
            FinalMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        }

        //number of tiles in the world data
        int quads = Mathf.FloorToInt(meshDimensions.x * meshDimensions.y);

        //4 vertexes per quad, non-shared.
        Vector3[] MeshVerts = new Vector3[quads * 4];
        Vector2[] MeshUVs = new Vector2[MeshVerts.Length];

        int iVertCount = 0;

        //Iterates through each possible position in the vertex array and creates a Vector3 point in it.
        for (int y = 0; y < meshDimensions.y; y++)
        {
            for (int x = 0; x < meshDimensions.x; x++)
            {
                //now entering baby hell
                //represents a quad, counter-clockwise.
                MeshVerts[iVertCount + 0] = new Vector3(x, 0, y);
                MeshVerts[iVertCount + 1] = new Vector3(x + 1, 0, y);
                MeshVerts[iVertCount + 2] = new Vector3(x, 0, y + 1);
                MeshVerts[iVertCount + 3] = new Vector3(x + 1, 0, y + 1);

                MeshUVs[iVertCount + 0] = new Vector2(0f, 0f);
                MeshUVs[iVertCount + 1] = new Vector2(0f, 0f);
                MeshUVs[iVertCount + 2] = new Vector2(0f, 0f);
                MeshUVs[iVertCount + 3] = new Vector2(0f, 0f);

                iVertCount += 4;

            }
        };

        //3 vertexes per triangle, 2 triangles per quad.
        int[] tris = new int[quads * 6];
        int iIndexCount = iVertCount = 0;

        //designate the triangles as different vertex points
        for (int i = 0; i < quads; i++)
        {
            //bottom-tri
            tris[iIndexCount + 0] += (iVertCount + 0);
            tris[iIndexCount + 1] += (iVertCount + 2);
            tris[iIndexCount + 2] += (iVertCount + 1);
            //top-tri
            tris[iIndexCount + 3] += (iVertCount + 2);
            tris[iIndexCount + 4] += (iVertCount + 3);
            tris[iIndexCount + 5] += (iVertCount + 1);

            //move over 4 vertexes (1 quad/2 tris)
            iVertCount += 4; iIndexCount += 6;

        }

        FinalMesh.vertices = MeshVerts;
        FinalMesh.triangles = tris;
        FinalMesh.uv = MeshUVs;
        FinalMesh.RecalculateNormals();

        return CreateMeshComponents(FinalMesh, meshMaterial);

    }

    public static GameObject CreateMeshComponents(Mesh mesh, string meshMaterial)
    {
        GameObject MeshGameObject = new GameObject();
        MeshGameObject.transform.name = "GeneratedMesh";

        MeshRenderer meshRenderer = MeshGameObject.AddComponent<MeshRenderer>();



        meshRenderer.sharedMaterial = AssetLoader.MaterialLibrary[meshMaterial];

        MeshFilter meshFilter = MeshGameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        return MeshGameObject;

    }


}
