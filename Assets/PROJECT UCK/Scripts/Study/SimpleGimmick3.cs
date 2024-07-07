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
            // To do : �浹�� ������Ʈ�� �÷��̾� �̶��?
            // => �÷��̾ Gimmick ���� �ȿ� �ִ� ���� Bridge�� �����ش�.
            // => �÷��̾ Gimmick ���� �ȿ� �ִ� ����
            // BridgeTarget ������Ʈ�� ���̾ RenderMask�� �����Ѵ�.

            if (collision.gameObject.CompareTag("Player"))
            {
                bridgeTarget.layer = LayerMask.NameToLayer(defaultLayerName);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            // To do : �浹�� ������Ʈ�� �÷��̾� �̶��?
            // => �÷��̾ Gimmick ������ ���������� Bridge�� �����.
            // => �÷��̾ Gimmick ������ ����������
            // BridgeTarget ������Ʈ�� ���̾ TransparentMask�� �����Ѵ�.

            if (collision.gameObject.CompareTag("Player"))
            {
                bridgeTarget.layer = LayerMask.NameToLayer(transparentLayerName);
            }
        }
    }
}

