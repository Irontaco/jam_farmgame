using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Handles movement modifiers for anything that is supposed to move.
interface Movement
{

    float BaseMovementSpeed { get; set; }

    //result of any modifications on the BaseMovementSpeed
    float CurrentMovementSpeed { get; set; }

    //final multiplier applied onto BaseMovementSpeed
    float CummulativeMovementSpeedMultiplier { get; set; }

    float ApplyMovementSpeedModifier(float modifier);
    void FreezeInPlace(float freezeTime);

}
