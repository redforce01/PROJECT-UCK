using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public interface IDamage
    {
        void TakeDamage(GameObject attacker, float damage);
    }
}
