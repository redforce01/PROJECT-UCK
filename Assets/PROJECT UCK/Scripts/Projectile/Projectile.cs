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
                // To do : �Ѿ��� �浹���� ���� ó��
                // 1. Character�� �ε��ƴ°�?
                // 2. or �ٸ� �繰ü�� �ε��ƴ°�?

                if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
                {
                    // To do : Character�� �������� ������ ó��
                    CharacterBase hitCharacter = other.transform.root.GetComponent<CharacterBase>();
                    if (hitCharacter != null)
                    {
                        hitCharacter.TakeDamage(10f);
                    }
                }
                else
                {
                    // To do : �ٸ� �繰ü�� ���� ó��
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
