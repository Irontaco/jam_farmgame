using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


//Keeps track and controls all enemy-spawning behaviors.
public class EnemyController : MonoBehaviour
{

    //The current GSM.
    public GameStateManager GameStateManager;
    public TickManager TickManager;

    //Keeps track of currently living aliens.
    public List<Alien> LivingAliens;

    public int WaveEnemyCount;
    public int EnemiesSpawned;

    //Define the ammount of waves as a result of the current level we're in and the total level ticks.
    //For now let's just assume 8 waves.
    public int CurrentLevelWaves;
    public int CurrentLevelElapsedWaves;

    //Tick limit until we begin the next wave of enemies.
    public int TicksUntilNextWave;

    //Currently elapsed ticks.
    public int PreWaveElapsedTicks;
    public int WaveElapsedTicks;

    //Time within to spawn the enemies of the current wave.
    public int WaveSpawnTicks;

    public bool WaveStarted;

    void Start()
    {
        GameStateManager = gameObject.GetComponent<GameStateManager>();
        TickManager = gameObject.GetComponent<TickManager>();

        CurrentLevelWaves = 8;
        TicksUntilNextWave = TickManager.LevelTicks / CurrentLevelWaves;

        //We get half the time until the next wave to spawn enemies.
        WaveSpawnTicks = TicksUntilNextWave / 2;

        LivingAliens = new List<Alien>();
    }

    void Update()
    {
        if(EnemiesSpawned == WaveEnemyCount)
        {
            WaveStarted = false;
        }

        //Wave logic
        #warning TODO: Determine if we generate enemies dynamically or do prepare the waves beforehand.
        if (WaveStarted)
        {

            //Each wave consists of enemies being spawned in small groups every once in a while *within* the WaveSpawnTicks.
            if(WaveElapsedTicks > WaveSpawnTicks / 10)
            {
                //Determine how many enemies we spawn on this current spawn cycle...
                int enemiesToSpawn = WaveEnemyCount / 4;

                //Spawn enemies then reset the waveElapsedTicks timer.
                for (int i = 0; i <= enemiesToSpawn; i++)
                {

                    Debug.Log("Enemy spawned!");
                    EnemiesSpawned++;
                    //GameObject enemyspawn = GameObject.Instantiate(AssetLoader.PrefabLibrary["Alien"]);

                    //enemy stuff blabla

                    
                }

                WaveElapsedTicks = 0;
            }

        }

        //We've hit the tick limit before the next wave - begin spawning enemies and reset our timer.
        if(PreWaveElapsedTicks > TicksUntilNextWave)
        {
            WaveStarted = true;
            WaveEnemyCount = 4 * (1+CurrentLevelElapsedWaves);

            PreWaveElapsedTicks = 0;
        }

    }


}
