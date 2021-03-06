using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SaberHighlight
{
    internal class HighlightSettingsView : BSMLResourceViewController
    {
        public override string ResourceName => string.Join(".", GetType().Namespace, GetType().Name);

        [UIValue("enabled")]
        protected bool SettingsEnabled
        {
            get => Plugin.CurrentSettings.Enabled;
            set => Plugin.CurrentSettings.Enabled = value;
        }

        [UIValue("summary-on-exit")]
        protected bool SettingsSummaryOnExit
        {
            get => Plugin.CurrentSettings.ShowSummaryOnExit;
            set => Plugin.CurrentSettings.ShowSummaryOnExit = value;
        }

        [UIValue("save-pass")]
        protected bool SettingsSavePass
        {
            get => Plugin.CurrentSettings.SavePass;
            set => Plugin.CurrentSettings.SavePass = value;
        }

        [UIValue("save-fail")]
        protected bool SettingsSaveFail
        {
            get => Plugin.CurrentSettings.SaveFail;
            set => Plugin.CurrentSettings.SaveFail = value;
        }

        [UIValue("save-exit")]
        protected bool SettingsSaveExit
        {
            get => Plugin.CurrentSettings.SaveExit;
            set => Plugin.CurrentSettings.SaveExit = value;
        }

        [UIValue("offset-start")]
        protected int SettingsOffsetStart
        {
            get => Plugin.CurrentSettings.OffsetStart;
            set => Plugin.CurrentSettings.OffsetStart = value;
        }

        [UIValue("offset-end")]
        protected int SettingsOffsetEnd
        {
            get => Plugin.CurrentSettings.OffsetEnd;
            set => Plugin.CurrentSettings.OffsetEnd = value;
        }
    }
}
