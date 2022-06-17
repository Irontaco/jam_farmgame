using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Atom is used as a base for anything that can exist in-game. 
//Holds basic position data + contents.
public interface IAtom
{
    //Positional markers...
    int X { get; set; }
    int Y { get; set; }
    int Z { get; set; }

    string Examine { get; }

    //List of anything contained within this specific unit.
    List<object> Contents { get; }

}

public static class Extensions
{
    public static Vector3 GetWorldspacePosition(this IAtom atom)
    {
        Vector3 position = new Vector3(atom.X, atom.Y, atom.Z);

        return position;
    }

}
