using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UCK
{
    public class SampleCannon : MonoBehaviour
    {
        public Collider[] physicsEffectObjects;

        public Transform fireStartPoint;
        public float firePower;
        public float fireAngle;

        public Rigidbody bullet;
        public LineRenderer lineRenderer;

        public int simulationStpes = 300;


        Scene simulationScene;
        PhysicsScene physicsScene;

        private void Start()
        {
            simulationScene = SceneManager.CreateScene(
                "Simulation Scene", 
                new CreateSceneParameters(LocalPhysicsMode.Physics3D));

            physicsScene = simulationScene.GetPhysicsScene();

            for (int i = 0; i < physicsEffectObjects.Length; i++)
            {
                GameObject ghostObject = Instantiate(
                    physicsEffectObjects[i].gameObject, 
                    physicsEffectObjects[i].transform.position, 
                    physicsEffectObjects[i].transform.rotation);

                if (ghostObject.TryGetComponent(out Renderer ghostRenderer))
                {
                    ghostRenderer.enabled = false;
                }

                SceneManager.MoveGameObjectToScene(ghostObject, simulationScene);
            }
        }

        private void Update()
        {
            Simulation();
        }

        public void Simulation()
        {
            Rigidbody ghostBullet = Instantiate(bullet, fireStartPoint.position, fireStartPoint.rotation);
            SceneManager.MoveGameObjectToScene(ghostBullet.gameObject, simulationScene);
            ghostBullet.gameObject.SetActive(true);
            ghostBullet.AddForce(ghostBullet.transform.forward * firePower, ForceMode.Impulse);

            lineRenderer.positionCount = simulationStpes;
            for (int i = 0; i < simulationStpes; i++)
            {
                physicsScene.Simulate(Time.fixedDeltaTime);
                lineRenderer.SetPosition(i, ghostBullet.transform.position);
            }

            Destroy(ghostBullet.gameObject);
        }
    }
}
