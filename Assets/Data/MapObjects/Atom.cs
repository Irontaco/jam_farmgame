using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Atom is used as a base for anything that can exist in-game. 
//It has the very basic data of every object and entity.
//This is basis of the data layer!
public interface IAtom
{
    //Positional markers...
    int X { get; set; }
    int Y { get; set; }
    int Z { get; set; }

    string Examine { get; set; }

    //List of anything contained within this specific unit.
    List<object> Contents { get;  set; }
}

public static class Extensions
{
    public static Vector3 GetWorldspacePosition(this IAtom atom)
    {
        Vector3 position = new Vector3(atom.X, atom.Y, atom.Z);

        return position;
    }

}
