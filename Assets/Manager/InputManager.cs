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
        None = 0,
        Up = 1 << 0,
        Down = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3
    }

    public InputManager(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
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
            //{ Direction.Up | Direction.Left, new Vector2(-1f, 1f).normalized },
            { Direction.Left, new Vector2(-1f, 0f) },
            //{ Direction.Left | Direction.Down, new Vector2(-1f, -1f).normalized },
            { Direction.Down, new Vector2(0f, -1f) },
            //{ Direction.Down | Direction.Right, new Vector2(1f, -1f).normalized },
            { Direction.Right, new Vector2(1f, 0f) },
            //{ Direction.Right | Direction.Up, new Vector2(1f, 1f).normalized }
        }.ToImmutableDictionary();
    public static Direction MovementToDirection(Vector2 movement)
    {
        if (movement.sqrMagnitude < float.Epsilon * 4f) { return Direction.None; }
        movement = movement.normalized;
        return dirToMovementExtremes
            .OrderByDescending(kvp => Vector2.Dot(kvp.Value, movement))
            .First().Key;
    }

    public bool Interact()
    {
        return binds.Interact.Hit;
    }
}