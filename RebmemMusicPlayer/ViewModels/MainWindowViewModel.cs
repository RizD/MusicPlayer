using RebmemMusicPlayer.Models;
using RebmemMusicPlayer.MVVM;
using RebmemMusicPlayer.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RebmemMusicPlayer.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private XmlFileService _xmlFileService;

        public ICommand ExitApplicationCommand { get; }
        public ICommand LoadFileCommand { get; }
        public ICommand LoadFolderCommand { get; }
        public ICommand SavePlaylistCommand { get; }
        public ICommand LoadPlaylistCommand { get; }
        public ICommand NextTrackCommand { get; }
        public ICommand PreviousTrackCommand { get; }

        public ObservableCollection<AudioFileModel> Playlist
        {
            get => Get<ObservableCollection<AudioFileModel>>();
            set => Set(value);
        }

        public AudioFileModel SelectedAudioFile
        {
            get => Get<AudioFileModel>();
            set => Set(value);
        }

        public MainWindowViewModel()
        {
            ExitApplicationCommand = new RelayCommand(ExitApplication);
            LoadFileCommand = new RelayCommand(LoadFile);
            LoadFolderCommand = new RelayCommand(LoadFolder);
            SavePlaylistCommand = new RelayCommand(SavePlaylist);
            LoadPlaylistCommand = new RelayCommand(LoadPlaylist);
            NextTrackCommand = new RelayCommand(NextTrack);
            PreviousTrackCommand = new RelayCommand(PreviousTrack);
            Playlist = new ObservableCollection<AudioFileModel>();
            SelectedAudioFile = new AudioFileModel();
            _xmlFileService = new XmlFileService();
        }

        private void LoadFile()
        {
            FileSystemService fileSystemService = new FileSystemService();
            AudioFileModel? audioFileToAdd = fileSystemService.LoadAudioFile();

            if (audioFileToAdd.Path != null)
            {
                Playlist.Add(audioFileToAdd);
            }
        }

        private void LoadFolder()
        {
            FileSystemService fileSystemService = new FileSystemService();
            ClearPlaylist();
            Playlist = fileSystemService.LoadAudioFolder();
            SetSelectedAudioFile();
        }

        private void NextTrack()
        {
            int currentTrackIndex = Playlist.IndexOf(SelectedAudioFile);
            SetTrack(currentTrackIndex + 1);
        }

        private void PreviousTrack()
        {
            int currentTrackIndex = Playlist.IndexOf(SelectedAudioFile);
            SetTrack(currentTrackIndex - 1);
        }

        private void SetTrack(int nextTrackIndex)
        {
            int currentTrackIndex = Playlist.IndexOf(SelectedAudioFile);

            if (currentTrackIndex != -1)
            {
                if (Playlist.Count > 0)
                {
                    try
                    {
                        SelectedAudioFile = Playlist[nextTrackIndex];
                    }
                    catch (System.Exception)
                    {
                        SelectedAudioFile = Playlist[0];
                    }
                }
            }
        }

        private void SavePlaylist()
        {
            FileSystemService fileSystemService = new FileSystemService(_xmlFileService);
            if (Playlist.Any())
            {
                fileSystemService.SavePlaylist(Playlist);
            }
            else
            {
                MessageBox.Show("Playlist is empty, please add a song to the playlist before trying to save.");
            }
        }

        private void LoadPlaylist()
        {
            FileSystemService fileSystemService = new FileSystemService(_xmlFileService);
            ClearPlaylist();
            Playlist = fileSystemService.LoadPlaylist();
            SetSelectedAudioFile();
        }

        private void ClearPlaylist()
        {
            if (Playlist != null)
            {
                Playlist.Clear();
            }
        }

        private void SetSelectedAudioFile()
        {
            if (Playlist != null && Playlist.Count != 0)
            {
                SelectedAudioFile = Playlist.First();
            }
        }

        private void ExitApplication()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?","Are you sure?",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question,
            MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
