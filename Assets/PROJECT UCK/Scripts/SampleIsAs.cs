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
            // dogAnimal 에 animal_A 를 Dog 로 변환한 결과를 대입
            // as 연산자는 변환에 실패하면 null 을 반환
            Dog dogAnimal = animal_A as Dog;
                        
            Dog dog_A = new Dog();
            Animal animal_B = dog_A as Animal;


        }
    }
}
