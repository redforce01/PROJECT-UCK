using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class CharacterBase : MonoBehaviour
    {
        public float currentHP;
        public float maxHP;

        public System.Action<float, float> OnChangedHP;

        private void Start()
        {
            currentHP = maxHP;
        }

        public void TakeDamage(float damage)
        {
            currentHP -= damage;

            OnChangedHP?.Invoke(currentHP, maxHP);

            if (currentHP <= 0)
            {
                // 죽는것에 대한 처리
                // 예) 죽는 모션을 재생한다.

                Destroy(gameObject);
            }
        }
    }
}
