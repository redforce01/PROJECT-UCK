using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    [System.Serializable]
    public class SampleWarriorCharacter : SampleCharacter
    {
        public override void ExecuteSkill()
        {
        }
    }

    [System.Serializable]
    public class SampleCharacter
    {
        public string name;
        public string skillName;

        public virtual void ExecuteSkill()
        {
        }
    }

    public class SampleInheritance : MonoBehaviour
    {
        public SampleCharacter characterA;
        public SampleWarriorCharacter warriorCharacter;

        private void Start()
        {
            characterA = new SampleCharacter();
            characterA.name = "Character A";
            characterA.skillName = "Fireball";
            characterA.ExecuteSkill();

            warriorCharacter = new SampleWarriorCharacter();
            warriorCharacter.name = "Warrior";
            warriorCharacter.skillName = "smash";
            warriorCharacter.ExecuteSkill();
        }
    }
}
