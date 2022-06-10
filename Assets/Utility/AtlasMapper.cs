using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates UV coords based on a given atlas.
/// </summary>
public class AtlasMapper
{

    /// <summary>
    /// For a given sprite atlas, subdivides it into usable UV coordinates for mapping to a mesh.
    /// Simplified, it splits up an atlas into sprites, rapidly assignable through it's number in the atlas grid.
    /// It's ordered bottom first, clockwise!
    /// </summary>
    /// <param name="spritePerSide">How many sprites are there per side?</param>
    /// <param name="atlas">Sprite atlas to split</param>
    /// <returns></returns>
    public static Dictionary<int, List<Vector2>> SubdivideAtlas(int spritePerSide, Sprite atlas)
    {
        Dictionary<int, List<Vector2>> TiledSprites = new Dictionary<int, List<Vector2>>();

        float uvStep = 1f / spritePerSide;

        int currentSprite = 0;

        //"go through the grid" of all needed UV coords...
        for (float y = 0f; y < spritePerSide; y++)
        {
            for (float x = 0f; x < spritePerSide; x++)
            {

                List<Vector2> currentUVList = new List<Vector2>();

                float currentStepX = x / spritePerSide;
                float currentStepY = y / spritePerSide;

                Vector2 uv_bottomleft = new Vector2(currentStepX, currentStepY);
                Vector2 uv_bottomright = new Vector2(currentStepX + uvStep, currentStepY);
                Vector2 uv_topleft = new Vector2(currentStepX, currentStepY + uvStep);
                Vector2 uv_topright = new Vector2(currentStepX + uvStep, currentStepY + uvStep);

                currentUVList.Add(uv_bottomleft);
                currentUVList.Add(uv_bottomright);
                currentUVList.Add(uv_topleft);
                currentUVList.Add(uv_topright);

                TiledSprites.Add(currentSprite, currentUVList);

                currentSprite++;
            }
        }

        return TiledSprites;
    }
}
