using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SaberHighlight
{
    class HighlightsController : MonoBehaviour
    {
        public static HighlightsController Instance { get; private set; }

        private const float HideAfterSeconds = 3f;
        private const float ThresholdInPixels = 3f;

        private HighlightFlowCoordinator _flowCoordinator;
        private float _lastTime;
        private Vector3 _lastMousePos;
        private bool _shouldReset = true;

        public void Awake()
        {
            if (Instance != null)
            {
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this);
            Instance = this;

            MenuButtons.instance.RegisterButton(new MenuButton("Highlights", "Setup SaberHightlight here!", MenuButtonPressed, true));
        }

        public void Start()
        {
            _lastTime = Time.timeSinceLevelLoad;
            _lastMousePos = Input.mousePosition;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                Highlight.ShowSummary();
            }

            if (Plugin.CurrentSettings.HideMouse)
            {
                var dx = Input.mousePosition - _lastMousePos;
                var move = (dx.sqrMagnitude > (ThresholdInPixels * ThresholdInPixels));
                _lastMousePos = Input.mousePosition;

                if (move)
                    _lastTime = Time.timeSinceLevelLoad;

                Cursor.visible = (Time.timeSinceLevelLoad - _lastTime) < HideAfterSeconds;
            }
            else
            {
                if (_shouldReset)
                {
                    Cursor.visible = true;
                    _shouldReset = false;
                }
            }
        }

        private void MenuButtonPressed()
        {
            if (_flowCoordinator == null)
                _flowCoordinator = BeatSaberMarkupLanguage.BeatSaberUI.CreateFlowCoordinator<HighlightFlowCoordinator>();
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinatorOrAskForTutorial(_flowCoordinator);
        }
    }
}
