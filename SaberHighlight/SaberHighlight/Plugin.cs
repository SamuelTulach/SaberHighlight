using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using Zenject;
using SiraUtil.Zenject;
using NVIDIA;

namespace SaberHighlight
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        internal static Settings CurrentSettings;

        [Init]
        public void Init(Config config, IPALogger logger, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            CurrentSettings = config.Generated<Settings>();

            Highlights.HighlightScope[] requiredScopes = new
            Highlights.HighlightScope[2]
            {
                Highlights.HighlightScope.Highlights,
                Highlights.HighlightScope.HighlightsRecordVideo,
            };

            var status = Highlights.CreateHighlightsSDK("SaberHighlight", requiredScopes);
            if (status != Highlights.ReturnCode.SUCCESS)
            {
                Log.Critical($"Failed to initialize highlights! ({status})");
                Highlights.UpdateLog();
                return;
            }

            Highlights.RequestPermissions(Highlight.LogCallback);

            Highlights.HighlightDefinition[] highlightDefinitions = new Highlights.HighlightDefinition[1];

            highlightDefinitions[0].Id = "MAP_PLAY";
            highlightDefinitions[0].HighlightTags = Highlights.HighlightType.Achievement;

            highlightDefinitions[0].Significance = Highlights.HighlightSignificance.Good;

            highlightDefinitions[0].UserDefaultInterest = true;
            highlightDefinitions[0].NameTranslationTable = new Highlights.TranslationEntry[] 
            {
                new Highlights.TranslationEntry ("en-US", "Map play"),
            };

            Highlights.ConfigureHighlights(highlightDefinitions, "en-US", Highlight.LogCallback);

            Highlights.OpenGroupParams ogp1 = new Highlights.OpenGroupParams();
            ogp1.Id = "MAP_PLAY_GROUP";
            ogp1.GroupDescriptionTable = new Highlights.TranslationEntry[] 
            {
                new Highlights.TranslationEntry ("en-US", "Map play group"),
            };

            Highlights.OpenGroup(ogp1, Highlight.LogCallback);

            zenjector.OnGame<Installer>(false);

            Log.Info("Loaded.");
        }

        [OnStart]
        public void OnApplicationStart()
        {
            new GameObject("SummaryController").AddComponent<HighlightsController>();
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            if (Plugin.CurrentSettings.Enabled && Plugin.CurrentSettings.ShowSummaryOnExit)
                Highlight.ShowSummary();

            Highlights.ReleaseHighlightsSDK();
        }
    }
}
