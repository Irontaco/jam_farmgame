using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Base vars and methods for health management in entities.
internal interface IHealth
{

    float Health { get; set; }
    float CurrentHealth { get; set; }
    bool Dead { get; set; }
    bool Shielded { get; set; }

    void TakeDamage(float dmgAmmount);
    void Heal(float healAmmount);
    void Die();

}
