using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    private GameStateManager gameStateManager;
    private Animator spriteAnimator;
    private Vector3 spriteRelativePosition;
    private Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = FindObjectOfType<GameStateManager>();
        camera = FindObjectOfType<Camera>();
        spriteAnimator = GetComponentInChildren<Animator>();
        spriteRelativePosition = spriteAnimator.transform.position - transform.position;
    }

    private static ImmutableDictionary<InputManager.Direction, int> CreateDirectionAnimAssoc(
        params (InputManager.Direction Dir, string AnimName)[] mapping)
    {
        return mapping
            .Select(item => (item.Dir, Animator.StringToHash(item.AnimName)))
            .ToImmutableDictionary();
    }

    private static readonly ImmutableDictionary<InputManager.Direction, int> dirToWalkAnim
        = CreateDirectionAnimAssoc(
            (InputManager.Direction.Up, "Player_walk_up"),
            (InputManager.Direction.Down, "Player_walk"),
            (InputManager.Direction.Left, "Player_walk_side"),
            (InputManager.Direction.Right, "Player_walk_side_2"));
    
    private static readonly ImmutableDictionary<InputManager.Direction, int> dirToIdleAnim
        = CreateDirectionAnimAssoc(
            (InputManager.Direction.Up, "Player_idle_up"),
            (InputManager.Direction.Down, "Player_idle"),
            (InputManager.Direction.Left, "Player_idle_side"),
            (InputManager.Direction.Right, "Player_idle_side_2"),
            (InputManager.Direction.None, "Player_idle"));

    private InputManager.Direction prevWalkDir = InputManager.Direction.None;
    
    // FixedUpdate is called once per tick
    void FixedUpdate()
    {
        UpdateMovement();
        UpdateAnimation();
        UpdateCamera();
    }

    private void UpdateMovement()
    {
        var inputManager = gameStateManager.InputManager;
        var movement = inputManager.GetMovement();
        transform.position += new Vector3(movement.x * 0.1f, 0f, movement.y * 0.1f);
    }
    
    private void UpdateAnimation()
    {
        var inputManager = gameStateManager.InputManager;
        var movement = inputManager.GetMovement();
        
        void playAnimation(int newState)
        {
            // The walking animations mess with the sprite position
            // so reset it every time we switch
            spriteAnimator.transform.position = transform.position + spriteRelativePosition;
            spriteAnimator.Play(newState);
        }
        
        var walkDir = InputManager.MovementToDirection(movement);
        if (walkDir != InputManager.Direction.None)
        {
            int newState = dirToWalkAnim[walkDir];
            int prevState = spriteAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            if (newState != prevState)
            {
                playAnimation(newState);
            }
        }
        else if (prevWalkDir != InputManager.Direction.None)
        {
            int newState = dirToIdleAnim[prevWalkDir];
            playAnimation(newState);
        }
        prevWalkDir = walkDir;
    }

    private const float CameraDistanceFromPlayer = 8f;
    private void UpdateCamera()
    {
        camera.transform.position = transform.position - camera.transform.forward * CameraDistanceFromPlayer;
    }
}
