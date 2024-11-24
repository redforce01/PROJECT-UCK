using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    [System.Serializable]
    public class UserDataDTO { }

    [System.Serializable]
    public class PlayerSessionDTO : UserDataDTO
    {
        public Vector3 lastPosition;
        public float lastHP;
        public float lastMP;
    }

    [System.Serializable]
    public class PlayerDTO : UserDataDTO
    {
        public string nickName;
        public int level;
        public int money;
        public float exp;
    }
}
