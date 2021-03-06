using BeatSaberMarkupLanguage;
using HMUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberHighlight
{
    class HighlightFlowCoordinator : FlowCoordinator
    {
        private HighlightSettingsView _highlightView;

        public void Awake()
        {
            if (_highlightView == null)
                _highlightView = BeatSaberUI.CreateViewController<HighlightSettingsView>();
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {

                SetTitle("SaberHighlight");
                showBackButton = true;
                ProvideInitialViewControllers(_highlightView);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            var mainFlow = BeatSaberUI.MainFlowCoordinator;
            mainFlow.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Horizontal);
        }
    }
}
