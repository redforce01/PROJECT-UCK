using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class EnemyController : MonoBehaviour
    {
        public float explositionRadius = 3f;
        public float explositionDelayTime = 3f;
        public bool isExplositionStart = false;
        public MeshRenderer characterRenderer;
        public SensorBase sensor;

        private void Start()
        {
            sensor.OnDetectedCallback += OnDetected;
            sensor.OnLostSignalCallback += OnLostSignal;
        }

        private void OnDetected(GameObject gameObject)
        {
            ExplositionStart();
        }

        private void OnLostSignal(GameObject gameObject)
        {
            ExplositionStop();
        }

        private void Update()
        {
            if (isExplositionStart)
            {
                explositionDelayTime -= Time.deltaTime;
                if (explositionDelayTime <= 0)
                {
                    // To do : �ֺ��� �ִ� ĳ���͸� �˻��ؼ�. �������� �ִ� ó��
                    Collider[] overlapped = Physics.OverlapSphere(transform.position, explositionRadius, LayerMask.GetMask("Character"));
                    for (int i = 0; i < overlapped.Length; i++)
                    {
                        CharacterBase character = overlapped[i].GetComponent<CharacterBase>();
                        if (character != null)
                        {
                            character.TakeDamage(15f);
                        }
                    }

                    // �������� �ְ��� �ڱ� �ڽ� GameObject�� �ı��Ѵ�.
                    Destroy(gameObject);
                }
            }
        }

        public void ExplositionStart()
        {
            isExplositionStart = true;
            StartCoroutine(ColorChangeCoroutine());
        }

        public void ExplositionStop()
        {
            isExplositionStart = false;
            explositionDelayTime = 3f;
            StopAllCoroutines();
            characterRenderer.material.color = Color.white;
        }

        IEnumerator ColorChangeCoroutine()
        {
            while (explositionDelayTime > 0)
            {
                characterRenderer.material.color = Color.red;

                yield return new WaitForSeconds(0.1f);

                characterRenderer.material.color = Color.white;

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
