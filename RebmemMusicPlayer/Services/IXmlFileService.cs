using RebmemMusicPlayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RebmemMusicPlayer.Services
{
    public interface IXmlFileService
    {
        void WritePlaylist(string filePath, ObservableCollection<AudioFileModel> audioFiles);

        ObservableCollection<AudioFileModel> ReadPlaylist(XDocument xmlFile);

        XDocument LoadXmlDocument(string filePath);
    }
}
