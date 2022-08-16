using RebmemMusicPlayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RebmemMusicPlayer.Services
{
    public class XmlFileService : IXmlFileService
    {
        public void WritePlaylist(string filePath, ObservableCollection<AudioFileModel> audioFiles)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true
            };

            using XmlWriter writer = XmlWriter.Create(filePath, xmlWriterSettings);
            writer.WriteStartElement("Playlist");

            foreach (AudioFileModel audioFile in audioFiles)
            {
                writer.WriteStartElement("Track");
                writer.WriteElementString("TrackName", audioFile.Name);
                writer.WriteElementString("TrackPath", audioFile.Path);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public ObservableCollection<AudioFileModel> ReadPlaylist(XDocument xmlFile)
        {
            ObservableCollection<AudioFileModel> audioFiles = new ObservableCollection<AudioFileModel>();

            //Uses LINQ to select all "Track" nodes from the XML file given in Load().
            IEnumerable<XElement>? tracks = xmlFile.Root?.Elements("Track").Select(track => track);

            foreach (XElement track in tracks)
            {
                AudioFileModel audioFile = new AudioFileModel();
                audioFile.Name = track.Element("TrackName").Value.ToString();
                audioFile.Path = track.Element("TrackPath").Value.ToString();
                audioFiles.Add(audioFile);
            }

            return audioFiles;
        }

        public XDocument LoadXmlDocument(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            return XDocument.Load(filePath);
        }
    }
}
