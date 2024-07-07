using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    // Ŭ����,�޼���,�ʵ� ���� [ ] ���ȣ�� �ٿ��� �����ϴ� �� => Attribute ��� ��
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
        // �ʵ峪 �Ӽ����� [ ] ���ȣ�� ���̸� �̰��� �迭��.
        public BulletData[] bulletDatas;
    }
}

