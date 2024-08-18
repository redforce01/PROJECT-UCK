using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UCK
{
    public class EnemyController : MonoBehaviour
    {
        public int attackMotionIndex = 0;

        private CharacterBase characterBase;
        private SensorBase sensor;
        private CharacterBase targetCharacter = null;
        private List<CharacterBase> detectedCharacters = new List<CharacterBase>();
        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            //navMeshAgent.updatePosition = false;
            //navMeshAgent.updateRotation = false;

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
            if (targetCharacter != null)
            {
                float distance = Vector3.Distance(transform.position, targetCharacter.transform.position);
                if (distance < 2f)
                {
                    characterBase.Attack();
                }
                else
                {
                    navMeshAgent.SetDestination(targetCharacter.transform.position);
                }
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
