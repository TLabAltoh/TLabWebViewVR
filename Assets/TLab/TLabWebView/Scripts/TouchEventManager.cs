using UnityEngine;

namespace TLab.Android.WebView
{
    public class TouchEventManager : MonoBehaviour
    {
        [SerializeField] private RectTransform screenRect;
        [SerializeField] private TLabWebView tlabWebView;

        // rect transform
        private float[] screenEdge;
        private const int LEFT_IDX = 0;
        private const int RIGHT_IDX = 1;
        private const int BOTTOM_IDX = 2;
        private const int TOP_IDX = 3;
        private float[] screenSize;
        private const int VERTICAL_IDX = 0;
        private const int HORIZONTAL_IDX = 1;

        // event
        private const int TOUCH_DOWN = 0;
        private const int TOUCH_UP = 1;
        private const int TOUCH_MOVE = 2;

        // state
        private bool onTheWeb = false;

        void Start()
        {
            if (screenRect == null)
            {
                Debug.Log("toucheventmanager: screenRect is null");
                return;
            }

            // Screen Corners[0] : Left bottom
            // Screen Corners[1] : Left tops
            // Screen Corners[2] : Right tops
            // Screen Corners[3] : Right bottom
            Vector3[] screenCorners = new Vector3[4];
            screenRect.GetWorldCorners(screenCorners);

            for (int i = 0; i < screenCorners.Length; i++)
            {
                screenCorners[i] = RectTransformUtility.WorldToScreenPoint(Camera.main, screenCorners[i]);
                Vector3 tmp = screenCorners[i];
                Debug.Log("toucheventmanager: screenCorners[" + i + "]: " + tmp.x + ", " + tmp.y);
            }

            screenEdge = new float[4];
            screenEdge[LEFT_IDX] = screenCorners[0].x;
            screenEdge[RIGHT_IDX] = screenCorners[2].x;
            screenEdge[BOTTOM_IDX] = screenCorners[0].y;
            screenEdge[TOP_IDX] = screenCorners[1].y;

            screenSize = new float[2];
            screenSize[VERTICAL_IDX] = screenEdge[TOP_IDX] - screenEdge[BOTTOM_IDX];
            screenSize[HORIZONTAL_IDX] = screenEdge[RIGHT_IDX] - screenEdge[LEFT_IDX];
        }

        private int TouchHorizontal(float x)
        {
            return (int)((x - screenEdge[LEFT_IDX]) / screenSize[HORIZONTAL_IDX] * tlabWebView.WebWidth);
        }

        private int TouchVertical(float y)
        {
            return (int)((1.0f - (y - screenEdge[BOTTOM_IDX]) / screenSize[VERTICAL_IDX]) * tlabWebView.WebHeight);
        }

        void Update()
        {
            if (tlabWebView == null) return;

#if !UNITY_EDITOR
        foreach(Touch t in Input.touches)
        {
#if false
            float x = t.position.x;
            float y = t.position.y;
#endif

            int x = TouchHorizontal(t.position.x);
            int y = TouchVertical(t.position.y);

            int eventNum = (int)TouchPhase.Stationary;

            if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) eventNum = TOUCH_UP;
            else if (t.phase == TouchPhase.Began) eventNum = TOUCH_DOWN;
            else if (t.phase == TouchPhase.Moved) eventNum = TOUCH_MOVE;

            if (x > tlabWebView.WebWidth || x < 0 || y > tlabWebView.WebHeight || y < 0)
            {
                if (onTheWeb == true && t.phase == TouchPhase.Moved)
                    eventNum = TOUCH_UP;
                else
                    eventNum = (int)TouchPhase.Stationary;

                onTheWeb = false;
            }
            else
            {
                onTheWeb = true;
            }

#if false
            Debug.Log("toucheventmanager: mouse pos (" + x.ToString() + ", " + y.ToString() + ")");
#endif

            tlabWebView.TouchEvent(x, y, eventNum);
        }
#endif
        }
    }
}
