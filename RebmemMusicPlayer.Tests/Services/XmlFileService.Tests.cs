using RebmemMusicPlayer.Models;
using RebmemMusicPlayer.Services;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using Xunit;

namespace RebmemMusicPlayer.Tests.Services
{
    public class XmlFileServiceTests
    {
        private const string _playlistXml = @"
<Playlist>
  <Track>
    <TrackName>track1</TrackName>
    <TrackPath>C:\track1.mp3</TrackPath>
  </Track>
  <Track>
    <TrackName>track2</TrackName>
    <TrackPath>C:\track2.mp3</TrackPath>
  </Track>
  <Track>
    <TrackName>track3</TrackName>
    <TrackPath>C:\track3.mp3</TrackPath>
  </Track>
</Playlist>
";

        /// <summary>
        /// Tests that the ReadPlaylist() method returns the playlist and that it's not empty..
        /// </summary>
        [Fact]
        public void ReadPlaylist_ValidPlaylist_PlaylistNotEmpty()
        {
            ObservableCollection<AudioFileModel> audioFiles = new ObservableCollection<AudioFileModel>();

            XmlFileService xmlFileService = new XmlFileService();
            XDocument playListXml = XDocument.Parse(_playlistXml);
            audioFiles = xmlFileService.ReadPlaylist(playListXml);

            Assert.NotEmpty(audioFiles);

        }

        /// <summary>
        /// Tests that the ReadPlaylist() method returns the playlist with correct items.
        /// </summary>
        [Fact]
        public void ReadPlaylist_ValidPlaylist_PlaylistItemsMatchXml()
        {
            ObservableCollection<AudioFileModel> audioFiles = new ObservableCollection<AudioFileModel>();

            XmlFileService xmlFileService = new XmlFileService();
            XDocument playListXml = XDocument.Parse(_playlistXml);
            audioFiles = xmlFileService.ReadPlaylist(playListXml);

            Assert.Equal("track1", audioFiles[0].Name);
            Assert.Equal("C:\\track1.mp3", audioFiles[0].Path);
            Assert.Equal("track2", audioFiles[1].Name);
            Assert.Equal("C:\\track2.mp3", audioFiles[1].Path);
            Assert.Equal("track3", audioFiles[2].Name);
            Assert.Equal("C:\\track3.mp3", audioFiles[2].Path);
        }
    }
}
