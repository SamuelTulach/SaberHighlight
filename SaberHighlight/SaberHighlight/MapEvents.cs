using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace SaberHighlight
{
    class MapEvents : IInitializable, IDisposable
    {
        [InjectOptional] private GameplayCoreSceneSetupData _sceneData;
        [InjectOptional] private StandardLevelGameplayManager _gameplayManager;
        [InjectOptional] private PauseController _pauseController;

        public MapEvents() { }

        public void Initialize()
        {
            if (!(_sceneData is GameplayCoreSceneSetupData) 
                || !(_gameplayManager is StandardLevelGameplayManager) 
                || !(_pauseController is PauseController))
            {
                Plugin.Log.Critical("Some components not initialized!");
                return;
            }

            _gameplayManager.levelFailedEvent += LevelFailedEvent;
            _gameplayManager.levelFinishedEvent += LevelFinishedEvent;

            _pauseController.didReturnToMenuEvent += LevelQuitEvent;

            IBeatmapLevel levelData = _sceneData.difficultyBeatmap.level;
            Highlight.StartRecording(levelData.songName);
        }

        public void Dispose()
        {
            if (!(_sceneData is GameplayCoreSceneSetupData)
                || !(_gameplayManager is StandardLevelGameplayManager)
                || !(_pauseController is PauseController))
                return;

            _gameplayManager.levelFailedEvent -= LevelFailedEvent;
            _gameplayManager.levelFinishedEvent -= LevelFinishedEvent;

            _pauseController.didReturnToMenuEvent -= LevelQuitEvent;
        }

        private void LevelQuitEvent() 
        {
            if (Plugin.CurrentSettings.SaveExit)
                Highlight.SaveRecording();
        }

        private void LevelFailedEvent() 
        {
            if (Plugin.CurrentSettings.SaveFail)
                Highlight.SaveRecording();
        }

        private void LevelFinishedEvent() 
        {
            if (Plugin.CurrentSettings.SavePass)
                Highlight.SaveRecording();
        }
    }
}
