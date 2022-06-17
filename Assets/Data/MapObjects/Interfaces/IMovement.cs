using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//Handles movement modifiers for anything that is supposed to move.
interface IMovement
{

    float BaseMovementSpeed { get; set; }

    //result of any modifications on the BaseMovementSpeed
    float CurrentMovementSpeed { get; set; }

    //final multiplier applied onto BaseMovementSpeed
    float CummulativeMovementSpeedMultiplier { get; set; }

    float ApplyMovementSpeedModifier(float modifier);
    void FreezeInPlace(float freezeTime);

    //Make this entity move towards a specific column of tiles.
    void SwitchLanes(Vector3 moveTowards);

    //Use this for any detours or special movements that the entity should make.
    void PerformMovementSpecial();

    //when we are instantiated or we are stopped from walking by something - continue moving downwards.
    void BeginWalkingFowards();

}
