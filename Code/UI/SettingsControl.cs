using Godot;
using System;

namespace GA.GArkanoid.UI
{
	public partial class SettingsControl : Control
	{
		[Export] private Button _saveButton = null;
		[Export] private Button _cancelButton = null;
		[Export] private Button _okButton = null;

		#region Audio
		[Export] private AudioControl _masterAudioControl = null;
		[Export] private AudioControl _musicAudioControl = null;
		[Export] private AudioControl _sfxAudioControl = null;

		[Export] private string _masterBusName = null;
		[Export] private string _musicBusName = null;
		[Export] private string _sfxBusName = null;

		[Export] private string _masterDisplayName = null;
		[Export] private string _musicDisplayName = null;
		[Export] private string _sfxDisplayName = null;
		#endregion Audio

		public override void _EnterTree()
		{
			_saveButton.Pressed += Save;
			_cancelButton.Pressed += Close;

			_okButton.Pressed += Save;
			_okButton.Pressed += Close;

			if (_masterAudioControl != null)
			{
				_masterAudioControl.VolumeChanged += OnVolumeChanged;
			}
			if (_musicAudioControl != null)
			{
				_musicAudioControl.VolumeChanged += OnVolumeChanged;
			}
			if (_sfxAudioControl != null)
			{
				_sfxAudioControl.VolumeChanged += OnVolumeChanged;
			}
		}

		public override void _ExitTree()
		{
			_saveButton.Pressed -= Save;
			_cancelButton.Pressed -= Close;

			_okButton.Pressed -= Save;
			_okButton.Pressed -= Close;

			if (_masterAudioControl != null)
			{
				_masterAudioControl.VolumeChanged -= OnVolumeChanged;
			}
			if (_musicAudioControl != null)
			{
				_musicAudioControl.VolumeChanged -= OnVolumeChanged;
			}
			if (_sfxAudioControl != null)
			{
				_sfxAudioControl.VolumeChanged -= OnVolumeChanged;
			}
		}

		public override void _Ready()
		{
			if (_masterAudioControl != null)
			{
				// TODO: When settings data is implemented, read this from the data.
				// For now, let's query the volume from the AudioServer.
				SetUpAudioControl(_masterAudioControl, _masterBusName, _masterDisplayName);
			}

			if (_musicAudioControl != null)
			{
				SetUpAudioControl(_musicAudioControl, _musicBusName, _musicDisplayName);
			}

			if (_sfxAudioControl != null)
			{
				SetUpAudioControl(_sfxAudioControl, _sfxBusName, _sfxDisplayName);
			}
		}

		private void SetUpAudioControl(AudioControl audioControl, string busName, string displayName)
		{
			int audioBusIndex = AudioServer.GetBusIndex(busName);
			if (audioBusIndex >= 0)
			{
				float volume = AudioServer.GetBusVolumeDb(audioBusIndex);
				audioControl.Setup(busName, displayName, volume);
			}
		}

		private void OnVolumeChanged(string audioBus, float decibel)
		{
			// TODO: Add support for previewing audio and them saving (or restoring) volume 
			// separately.
			int audioBusIndex = AudioServer.GetBusIndex(audioBus);
			if (audioBusIndex < 0)
			{
				return;
			}

			AudioServer.SetBusVolumeDb(audioBusIndex, decibel);
		}

		public void Close()
		{
			GameManager.Instance.ActivatePreviousState();
		}

		public void Save()
		{
			// TODO: Implement saving Settings
			GD.Print("Saving the settings.");
		}
	}
}
