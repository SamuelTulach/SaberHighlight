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
    class SummaryController : MonoBehaviour
    {
        public static SummaryController Instance { get; private set; }

        private HighlightFlowCoordinator _flowCoordinator;

        public void Awake()
        {
            if (Instance != null)
            {
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this);
            Instance = this;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                Highlight.ShowSummary();
            }

            MenuButtons.instance.RegisterButton(new MenuButton("Highlights", "Setup SaberHightlight here!", MenuButtonPressed, true));
        }

        private void MenuButtonPressed()
        {
            if (_flowCoordinator == null)
                _flowCoordinator = BeatSaberMarkupLanguage.BeatSaberUI.CreateFlowCoordinator<HighlightFlowCoordinator>();
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinatorOrAskForTutorial(_flowCoordinator);
        }
    }
}
