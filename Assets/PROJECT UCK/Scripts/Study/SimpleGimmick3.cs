using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SimpleGimmick3 : MonoBehaviour
    {
        public GameObject bridgeTarget;

        public LayerMask renderMask;
        public LayerMask transparentMask;

        public string defaultLayerName = "Default";
        public string transparentLayerName = "InVisible";

        private void OnCollisionEnter(Collision collision)
        {
            // To do : 충돌한 오브젝트가 플레이어 이라면?
            // => 플레이어가 Gimmick 발판 안에 있는 동안 Bridge를 보여준다.
            // => 플레이어가 Gimmick 발판 안에 있는 동안
            // BridgeTarget 오브젝트의 레이어를 RenderMask로 변경한다.

            if (collision.gameObject.CompareTag("Player"))
            {
                bridgeTarget.layer = LayerMask.NameToLayer(defaultLayerName);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            // To do : 충돌한 오브젝트가 플레이어 이라면?
            // => 플레이어가 Gimmick 발판을 빠져나가면 Bridge를 숨긴다.
            // => 플레이어가 Gimmick 발판을 빠져나가면
            // BridgeTarget 오브젝트의 레이어를 TransparentMask로 변경한다.

            if (collision.gameObject.CompareTag("Player"))
            {
                bridgeTarget.layer = LayerMask.NameToLayer(transparentLayerName);
            }
        }
    }
}

