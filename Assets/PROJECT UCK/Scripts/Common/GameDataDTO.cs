using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    [System.Serializable]
    public class GameDataDTO { }


    [System.Serializable]
    public class CharacterStatData : GameDataDTO
    {
        [System.Serializable]
        public class CharacterStatDTO
        {
            public int level;
            public float moveSpeed;
            public float hp;
        }

        public List<CharacterStatDTO> characterStats = new List<CharacterStatDTO>();
    }
}
