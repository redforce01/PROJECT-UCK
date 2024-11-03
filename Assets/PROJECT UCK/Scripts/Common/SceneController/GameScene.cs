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

            // Ingame에서 사용할 UI를 띄워준다.
            // UIManager.Show<IngameUI>(UIList.IngameUI);
            // UIManager.Show<IndicatorUI>(UIList.IndicatorUI);
            // UIManager.Show<MinimapUI>(UIList.MinimapUI);
        }

        public override IEnumerator SceneEnd()
        {
            // Ingame에서 사용했던 UI를 숨겨준다.
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
