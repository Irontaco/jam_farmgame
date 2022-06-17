using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//GameManager should be used exclusively to instantiate everything we need in an orderly fashion.
//If anything fails, we can eventually point our finger back at this little bastard as the culprit.
public class GameStateManager : MonoBehaviour
{

    public WorldTileManager WorldTileManager { get; private set; }

    public TickManager TickManager { get; set; }

    public InputManager InputManager { get; private set; }
    
    public WorldData CurrentWorldData { get; private set; }

    public EnemyController EnemyController { get; private set; }

    public Config CurrentConfig { get; private set; } = Config.Default;

    //temp
    public Tile LastSelectedTile;
    public GameObject TileSelectText;

    public Canvas SoilDebugCanvas;
    public List<GameObject> SoilDebugTextGroup;

    void Start()
    { 
        //Create the world data, this should be done first always!
        CurrentWorldData = new WorldData(16 , 64);

        //Instantiate the WorldTileManager, which creates the necessary structures for the map and it's rendering to begin.
        WorldTileManager = new WorldTileManager(CurrentWorldData);

        InputManager = new InputManager(this);

        SoilDebugTextGroup = new List<GameObject>();

        TickManager = GetComponent<TickManager>();

        EnemyController = GetComponent<EnemyController>();
    }

    private void FixedUpdate()
    {
        Vector3 currMousePos = Input.mousePosition;

        var tile = LastSelectedTile = InputManager.GetTileAtMousePosition(out var rayHitPos);

        foreach (GameObject gameObject in SoilDebugTextGroup)
        {
            Destroy(gameObject);
        }
        SoilDebugTextGroup.Clear();

        if (tile != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, rayHitPos);

            WorldTileManager.ChangeTileType(tile, TileType.Virtual);
            WorldTileManager.WorldMeshData.UpdateWorldMesh();

            //Display tile value on TileSelectText
            TileSelectText.GetComponent<TextMeshProUGUI>().text = tile.Soil.Examine;
            TileSelectText.transform.position = new Vector3(currMousePos.x + 80, currMousePos.y + 60, currMousePos.z);

            //Draw Text on a 3x3 tile radius showing soil main value
            List<GameObject> NewSoilDebugTextGroup = new List<GameObject>();

            foreach(Soil soil in tile.Soil.SoilNeighbors)
            {
                if(soil != null)
                {
                    GameObject SoilText = new GameObject();
                    SoilText.AddComponent<TextMeshPro>().transform.SetParent(SoilDebugCanvas.transform);
                    SoilText.transform.name = "DebugSoilText";
                    SoilText.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
                    SoilText.transform.position = new Vector3(soil.X, soil.Y + 0.1f, soil.Z);
                    SoilText.transform.Rotate(new Vector3(90, 0, 0));

                    SoilText.GetComponent<TextMeshPro>().fontSize = 8;
                    SoilText.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
                    SoilText.GetComponent<TextMeshPro>().text = soil.AvailableEnergy.ToString();

                    NewSoilDebugTextGroup.Add(SoilText);
                }
            }

            SoilDebugTextGroup = NewSoilDebugTextGroup;
            
            WorldTileManager.ChangeTileType(LastSelectedTile, TileType.Floor);

        }

    }

    public void OnTickPassed()
    {
        EnemyController.PreWaveElapsedTicks++;

        if (EnemyController.WaveStarted)
        {
            EnemyController.WaveElapsedTicks++;
        }



    }
}
