using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class TitleScene : SceneBase
    {
        public override float SceneLoadingProgress { get; protected set; }
        public override IEnumerator SceneStart()
        {
            AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("TitleScene");

            while (!async.isDone)
            {
                yield return null;
                SceneLoadingProgress = async.progress;
            }

            // 타이틀 UI를 띄워준다.
            // UIManager.Show<TitleUI>(UIList.TitleUI);
        }

        public override IEnumerator SceneEnd()
        {
            // 타이틀 UI를 숨겨준다.
            UIManager.Hide<TitleUI>(UIList.TitleUI);

            AsyncOperation async = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("TitleScene");
            while(!async.isDone)
            {
                yield return null;
                SceneLoadingProgress = async.progress;
            }
        }
    }
}
