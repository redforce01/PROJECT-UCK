using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class Animal
    {
    }

    public class Dog : Animal
    {
    }

    public class Cat : Animal
    {
    }

    public class SampleIsAs : MonoBehaviour
    {
        private void Start()
        {
            Dog dog = new Dog();

            if (dog is Animal)
            {
                Debug.Log("dog is Animal");
            }

            Animal animal_A = new Animal();
            // dogAnimal �� animal_A �� Dog �� ��ȯ�� ����� ����
            // as �����ڴ� ��ȯ�� �����ϸ� null �� ��ȯ
            Dog dogAnimal = animal_A as Dog;
                        
            Dog dog_A = new Dog();
            Animal animal_B = dog_A as Animal;


        }
    }
}
