using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    // 클래스,메서드,필드 위에 [ ] 대괄호를 붙여서 선언하는 것 => Attribute 라고 함
    [System.Serializable] 
    public class BulletData
    {
        public bool isGravity;
        public float speed;
        public float lifeTime;
        public GameObject bulletPrefab;
    }

    public class BulletResourceData : MonoBehaviour
    {
        // 필드나 속성위에 [ ] 대괄호를 붙이면 이것은 배열임.
        public BulletData[] bulletDatas;
    }
}

