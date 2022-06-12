using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager
{
    private GameStateManager gameStateManager;
    private Config.Controls binds => gameStateManager.CurrentConfig.KeyBinds;

    [Flags]
    public enum Direction
    {
        None = 0,       //0000, no bits
        Up = 1 << 0,    //0001, least significant bit
        Down = 1 << 1,  //0010
        Left = 1 << 2,  //0100
        Right = 1 << 3  //1000, most significant bit
    }

    public InputManager(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }

    public GameObject GetGameObjectAtMousePosition(out Vector3 rayHitPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rayHit))
        {
            rayHitPos = rayHit.point;
            return rayHit.rigidbody.gameObject;
        }
        rayHitPos = default;
        return null;
    }

    public Tile GetTileAtMousePosition(out Vector3 rayHitPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane fieldPlane = new Plane(Vector3.up, 0f);
        if (fieldPlane.Raycast(ray, out float distance))
        {
            rayHitPos = ray.GetPoint(distance);
            int x = Mathf.RoundToInt(rayHitPos.x);
            int z = Mathf.RoundToInt(rayHitPos.z);
            if (x < 0 || x >= gameStateManager.WorldTileManager.WorldData.SizeX
                || z < 0 || z >= gameStateManager.WorldTileManager.WorldData.SizeZ)
            {
                return null;
            }
            return gameStateManager.WorldTileManager.GetTileAt(x, z);
        }
        rayHitPos = default;
        return null;
    }

    public Vector2 GetMovement()
    {
        Vector2 keyboardInput = new Vector2(
            x: (binds.Left.Held ? -1f : 0f) + (binds.Right.Held ? 1f : 0f),
            y: (binds.Up.Held ? 1f : 0f) + (binds.Down.Held ? -1f : 0f));
        Vector2 controllerInput = new Vector2(
            x: Input.GetAxisRaw("Horizontal"),
            y: Input.GetAxisRaw("Vertical"));
        Vector2 aggregate = keyboardInput + controllerInput;
        float magnitude = aggregate.magnitude;
        if (magnitude > 1f) { aggregate /= magnitude; }
        return aggregate;
    }

    private static readonly ImmutableDictionary<Direction, Vector2> dirToMovementExtremes =
        new Dictionary<Direction, Vector2>
        {
            { Direction.Up, new Vector2(0f, 1f) },
            { Direction.Left, new Vector2(-1f, 0f) },
            { Direction.Down, new Vector2(0f, -1f) },
            { Direction.Right, new Vector2(1f, 0f) },
            //uncomment these to implement diagonal animations
            //{ Direction.Up | Direction.Left, new Vector2(-1f, 1f).normalized },
            //{ Direction.Left | Direction.Down, new Vector2(-1f, -1f).normalized },
            //{ Direction.Down | Direction.Right, new Vector2(1f, -1f).normalized },
            //{ Direction.Right | Direction.Up, new Vector2(1f, 1f).normalized }
        }.ToImmutableDictionary();
    public static Direction MovementToDirection(Vector2 movement)
    {
        if (movement.sqrMagnitude < float.Epsilon * 4f) { return Direction.None; }
        movement = movement.normalized;
        return dirToMovementExtremes
            // Order the extremes based on how close they are to the input movement direction
            .OrderByDescending(kvp => Vector2.Dot(kvp.Value, movement))
            // Prioritize horizontal extremes
            .ThenByDescending(kvp => kvp.Key)
            .First().Key;
    }

    public bool Interact()
    {
        return binds.Interact.Hit;
    }
}