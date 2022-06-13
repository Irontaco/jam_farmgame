using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Data.MapObjects
{

    //Basic data-holder for plant-spots. Soil modifies Tiles as it's data changes!
    public class Soil : IAtom
    {
        //Tile this soil object occupies.
        private Tile Tile;

        private Plant Plant;

        //Something being able to be planted here depends on this value.
        public float AvailableEnergy;

        //Soil tiles eventually reach a max-cap.
        public float MaxEnergy = 100f;
        public float MinEnergy = 0f;

        private bool Dead => AvailableEnergy == MinEnergy;

        //All tiles slowly lose energy.
        public float BaseEnergyDraw = 0.1f;

        //The closer to maximum energy this is, the faster we deplete.
        //At 100 AvailableEnergy, the drain is 10 times faster than BaseEnergyDraw.
        public float RelativeEnergyDraw => 0.1f * AvailableEnergy / 10f;

        public int X { get => Tile.X; set => X = Tile.X; }
        public int Y { get => Tile.Y; set => X = Tile.Y; }
        public int Z { get => Tile.Z; set => X = Tile.Z; }

        public Soil[] SoilNeighbors
        {
            get
            {
                Soil[] SoilNeighbors = new Soil[8];

                for (int i = 0; i<SoilNeighbors.Length; i++)
                {
                    SoilNeighbors[i] = Tile.GetAllNeighbors(Tile)[i].Soil;
                }

                return SoilNeighbors;
            }
        }

        public string Examine
        {
            get
            {
                string retVal = $"{ Tile.GetType().Name } Tile at [{X},{Z}]";
                retVal += $" SOILDATA = [BaseEnergyDraw={BaseEnergyDraw} AvailableEnergy={AvailableEnergy} RelativeEnergyDraw={RelativeEnergyDraw}]";
                return retVal;
            }
        }

        public List<object> Contents { get; }


        public Soil(Tile tile)
        {
            this.Tile = tile;
            this.Contents = tile.Contents;

        }


    }
}