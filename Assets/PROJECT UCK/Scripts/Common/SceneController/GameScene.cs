using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class GameScene : SceneBase
    {
        public override float SceneLoadingProgress { get; protected set; }
        public override IEnumerator SceneStart()
        {
            AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GameScene");

            while (!async.isDone)
            {
                yield return null;
                SceneLoadingProgress = async.progress;
            }

            // Ingame���� ����� UI�� ����ش�.
            // UIManager.Show<IngameUI>(UIList.IngameUI);
            // UIManager.Show<IndicatorUI>(UIList.IndicatorUI);
            // UIManager.Show<MinimapUI>(UIList.MinimapUI);
        }

        public override IEnumerator SceneEnd()
        {
            // Ingame���� ����ߴ� UI�� �����ش�.
            // UIManager.Hide<IngameUI>(UIList.IngameUI);
            // UIManager.Hide<IndicatorUI>(UIList.IndicatorUI);
            // UIManager.Hide<MinimapUI>(UIList.MinimapUI);            

            AsyncOperation async = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("GameScene");
            while (!async.isDone)
            {
                yield return null;
                SceneLoadingProgress = async.progress;
            }
        }
    }
}
