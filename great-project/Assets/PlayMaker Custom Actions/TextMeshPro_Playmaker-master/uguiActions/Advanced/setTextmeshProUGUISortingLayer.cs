// (c) Eric Vander Wal, 2017 All rights reserved.
// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using TMPro;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("TextMesh Pro UGUI Advanced")]
    [Tooltip("Set sorting layers for Text Mesh Pro UGUI.")]
    public class setTextmeshProUGUISortingLayer : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(TextMeshProUGUI))]
        [Tooltip("Textmesh Pro component is required.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Set Geometry Sorting Order.")]
        [TitleAttribute("Geometry Sorting")]
        [ObjectType(typeof(VertexSortingOrder))]
        public FsmEnum geoSorting;

        [Tooltip("Check this box to preform this action every frame.")]
        public FsmBool everyFrame;

        private Vector4 margin4;

        TextMeshProUGUI meshproScript;

        public override void Reset()
        {
            gameObject = null;
            geoSorting = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);

            meshproScript = go.GetComponent<TextMeshProUGUI>();

            DoMeshChange();


            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                DoMeshChange();
            }
        }

        void DoMeshChange()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            if (meshproScript == null)
            {
                Debug.LogError("No textmesh pro ugui component was found on " + go);
                return;
            }

            meshproScript.geometrySortingOrder = (VertexSortingOrder) geoSorting.Value;
        }
    }
}