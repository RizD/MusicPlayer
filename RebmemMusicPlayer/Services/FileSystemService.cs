using RebmemMusicPlayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RebmemMusicPlayer.Services
{
    internal class FileSystemService
    {
        private XmlFileService _xmlFileService;

        public FileSystemService()
        {
        }

        public FileSystemService(XmlFileService xmlFileService)
        {
            _xmlFileService = xmlFileService; 
        }

        public AudioFileModel LoadAudioFile()
        {
            AudioFileModel audioFile = new AudioFileModel();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 Files (*.mp3)|*.mp3| All Files (*.*)|*.*";
            openFileDialog.CheckFileExists = true;
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                audioFile.Name = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                audioFile.Path = Path.GetFullPath(openFileDialog.FileName);
            }

            return audioFile;
        }

        public ObservableCollection<AudioFileModel> LoadAudioFolder()
        {
            ObservableCollection<AudioFileModel> audioFolder = new ObservableCollection<AudioFileModel>();

            using FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string[] directoryFiles = Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.mp3");

                foreach (string file in directoryFiles)
                {
                    AudioFileModel audioFile = new AudioFileModel();
                    audioFile.Name = Path.GetFileNameWithoutExtension(file);
                    audioFile.Path = Path.GetFullPath(file);
                    audioFolder.Add(audioFile);
                }
            }

            if (audioFolder.Count > 0)
            {
                return audioFolder;
            }

            return null;
        }

        public void SavePlaylist(ObservableCollection<AudioFileModel> playList)
        {
            using SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "MusicPlaylist";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.OverwritePrompt = true;
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                _xmlFileService.WritePlaylist(saveFileDialog.FileName, playList);
            }
        }

        public ObservableCollection<AudioFileModel> LoadPlaylist()
        {
            ObservableCollection<AudioFileModel> audioFiles = new ObservableCollection<AudioFileModel>();

            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.CheckFileExists = true;
            DialogResult result = openFileDialog.ShowDialog();
            XDocument xmlFile = _xmlFileService.LoadXmlDocument(openFileDialog.FileName);
            if (result == DialogResult.OK)
            {
                audioFiles = _xmlFileService.ReadPlaylist(xmlFile);
            }

            return audioFiles;
        }
    }

}
