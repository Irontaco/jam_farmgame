using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Alien : MonoBehaviour, IHealth, IMovement
{

    #region InterfaceVars
    public float Health { get; set; }
    public float CurrentHealth { get; set; }
    public bool Dead { get; set; }
    public bool Shielded { get; set; }
    public float BaseMovementSpeed { get; set; }
    public float CurrentMovementSpeed { get; set; }
    public float CummulativeMovementSpeedMultiplier { get; set; }
    #endregion

    public Animator SpriteAnimation { get; set; }
    public SpriteRenderer CurrentSprite { get; set; }

    //Use this to reference other aliens and enemies present on the map.
    public List<Alien> Comrades { get; set; }

    void Start()
    {

    }

    void Update()
    {

    }


    public void BeginWalkingFowards()
    {
    }

    public void Die()
    {
    }

    public void FreezeInPlace(float freezeTime)
    {
    }

    public void Heal(float healAmmount)
    {
    }

    public void PerformMovementSpecial()
    {
    }

    public void SwitchLanes(System.Numerics.Vector3 moveTowards)
    {
    }

    public void TakeDamage(float dmgAmmount)
    {
    }

    public void SwitchLanes(Vector3 moveTowards)
    {
        throw new NotImplementedException();
    }

    public float ApplyMovementSpeedModifier(float modifier)
    {
        throw new NotImplementedException();
    }
}

