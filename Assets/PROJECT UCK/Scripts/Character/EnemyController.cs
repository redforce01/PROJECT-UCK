using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class EnemyController : MonoBehaviour
    {
        public int attackMotionIndex = 0;

        private CharacterBase characterBase;
        private SensorBase sensor;

        private CharacterBase targetCharacter = null;
        private List<CharacterBase> detectedCharacters = new List<CharacterBase>();

        private void Awake()
        {
            characterBase = GetComponent<CharacterBase>();
            sensor = GetComponentInChildren<SensorBase>();
        }

        private void Start()
        {
            sensor.OnDetectedCallback += OnDetected;
            sensor.OnLostSignalCallback += OnLostSignal;

            Animator animator = GetComponent<Animator>();
            animator.SetInteger("AttackMotion", attackMotionIndex);
        }

        private void Update()
        {
            if (characterBase.IsAlive)
            {
                if (targetCharacter != null)
                {
                    if (targetCharacter.IsAlive)
                    {
                        float distance = Vector3.Distance(transform.position, targetCharacter.transform.position);
                        if (distance < 2f)
                        {
                            characterBase.Attack();
                        }
                        else
                        {
                            Vector3 direction = (targetCharacter.transform.position - transform.position).normalized;
                            characterBase.Move(new Vector2(direction.x, direction.z), 0);
                            characterBase.Rotate(targetCharacter.transform.position);
                        }
                    }
                    else
                    {
                        targetCharacter = null;
                    }
                }
                else
                {
                    characterBase.Move(Vector2.zero, 0f);
                }
            }
            else
            {
                characterBase.Move(Vector2.zero, 0f);
            }
        }

        private void OnDetected(GameObject detectedObject)
        {
            if (detectedObject.transform.root.TryGetComponent(out CharacterBase character))
            {
                detectedCharacters.Add(character);
                if (targetCharacter == null)
                {
                    targetCharacter = character;
                }
            }
        }

        private void OnLostSignal(GameObject lostObject)
        {
            if (lostObject.transform.root.TryGetComponent(out CharacterBase character))
            {
                detectedCharacters.Remove(character);
                if (targetCharacter == character)
                {
                    targetCharacter = null;
                }
            }
        }
    }
}
