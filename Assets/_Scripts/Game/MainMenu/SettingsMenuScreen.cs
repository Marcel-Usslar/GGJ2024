using System;
using System.Collections.Generic;
using CustomButton;
using Game.GameState;
using Game.UI;
using Game.Utility;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Game.MainMenu
{
    public class SettingsMenuScreen : BaseScreen
    {
        [Serializable]
        public class JoystickSettingsMapping
        {
            public JoystickSetting Setting;
            public RadioButtonComponent RadioButton;
        }

        [SerializeField] private ReactiveButton _backButton;
        [SerializeField] private ToggleComponent _musicToggle;
        [SerializeField] private Slider _joystickSizeSlider;
        [SerializeField] private List<JoystickSettingsMapping> _joystickSettingsMappings;

        protected override ReactiveProperty<bool> Visibility => MenuModel.Instance.ShowSettings;

        //Called through Unity
        public void OnSliderUpdated()
        {
            SettingsModel.Instance.JoystickSize.Value = _joystickSizeSlider.value;
        }

        protected override void OnStart()
        {
            _joystickSizeSlider.value = SettingsModel.Instance.JoystickSize.Value;

            SettingsModel.Instance.IsMusicMuted.RegisterCallback(muted => _musicToggle.IsOn = !muted);

            _musicToggle.Button
                .RegisterClickHandler(_ => SettingsModel.Instance.IsMusicMuted.Value = !SettingsModel.Instance.IsMusicMuted.Value);

            _backButton.RegisterClickHandler(_ => ReturnToMainMenu());

            _joystickSettingsMappings.ForEach(mapping => SettingsModel.Instance.JoystickSetting
                .RegisterCallback(setting => mapping.RadioButton.IsSelected = setting == mapping.Setting));

            _joystickSettingsMappings.ForEach(mapping => mapping.RadioButton.Button
                .RegisterClickHandler(_ => SettingsModel.Instance.JoystickSetting.Value = mapping.Setting));
        }

        private void OnDestroy()
        {
            SettingsModel.Instance.IsMusicMuted.UnregisterCallback(muted => _musicToggle.IsOn = !muted);

            _musicToggle.Button
                .UnregisterClickHandler(_ => SettingsModel.Instance.IsMusicMuted.Value = !SettingsModel.Instance.IsMusicMuted.Value);

            _backButton.UnregisterClickHandler(_ => ReturnToMainMenu());

            _joystickSettingsMappings.ForEach(mapping => SettingsModel.Instance.JoystickSetting
                .UnregisterCallback(setting => mapping.RadioButton.IsSelected = setting == mapping.Setting));

            _joystickSettingsMappings.ForEach(mapping => mapping.RadioButton.Button
                .UnregisterClickHandler(_ => SettingsModel.Instance.JoystickSetting.Value = mapping.Setting));
        }

        private void ReturnToMainMenu()
        {
            Hide();

            if (GameStateModel.Instance.IsPlaying)
                MenuModel.Instance.ShowPauseMenu.Value = true;
            else
                MenuModel.Instance.ShowMainMenu.Value = true;
        }
    }
}