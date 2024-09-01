using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class Projectile : MonoBehaviour
    {
        public LayerMask hitLayer;

        private Rigidbody rigid;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        public void SetForce(float force)
        {
            rigid.AddForce(transform.forward * force, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (hitLayer == (hitLayer | (1 << other.gameObject.layer)))
            {
                // To do : 총알이 충돌했을 때의 처리
                // 1. Character랑 부딪쳤는가?
                // 2. or 다른 사물체랑 부딪쳤는가?

                if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
                {
                    // To do : Character에 데미지를 입히는 처리
                    CharacterBase hitCharacter = other.transform.root.GetComponent<CharacterBase>();
                    if (hitCharacter != null)
                    {
                        hitCharacter.TakeDamage(10f);
                    }
                }
                else
                {
                    // To do : 다른 사물체에 대한 처리
                    EffectType targetEffectType = EffectType.Rock;
                    if (other.material.name.Contains("PhysicMaterial_Rock"))
                    {
                        targetEffectType = EffectType.Rock;
                    }
                    else if(other.material.name.Contains("PhysicMaterial_Wood"))
                    {
                        targetEffectType = EffectType.Wood;
                    }

                    UCKEffectSystem.Instance.SpawnEffect(
                        targetEffectType, 
                        transform.position, 
                        Quaternion.Inverse(transform.rotation));
                }

                Destroy(gameObject);
            }
        }
    }
}
