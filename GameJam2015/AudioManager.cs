using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.IO;
using System.Resources;

namespace GameJam2015
{
    public class AudioManager
    {
        public SoundPlayer player;
        public string stream, directory;
        public Stream s;

        public AudioManager(string path)
        {
            stream = path + "\\Audio";
            directory = Path.GetFullPath(stream);

            if (!Directory.Exists(directory))
            {
                Console.WriteLine("does not exist");
            }
            else
            {
                Console.WriteLine("Audio directory: {0}", directory);
            
            }
            player = new SoundPlayer();
            LoadAudio();
        }

        private void LoadAudioComplete()
        {
        }

        private void PlayAudioFromResource(Object sender, EventArgs e)
        {
        }

        private void LoadAudio()
        {
            foreach (string f in Directory.EnumerateFiles(directory))
            {
                try
                {

                    player.LoadAsync();
                    Console.WriteLine("Audio load for {0} was successful.", f);
                }
                catch
                {
                    Console.WriteLine("Error loading");
                }
            }
        }
    }
}
