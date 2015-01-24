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

        public AudioManager()
        {
            /*if (!Directory.Exists(directory))
            {
                Console.WriteLine("does not exist");
            }
            else
            {
                Console.WriteLine("Audio directory: {0}", directory);
            
            }*/
            player = new SoundPlayer();
        }

        private void PlayAudioFromResource(Object sender, EventArgs e)
        {
        }

        public void PlayBackground()
        {
            try
            {
                Console.WriteLine("play music");
                player.Play();
            }
            catch
            {
                Console.WriteLine("Error playing music");
            }

        }

        public void LoadAudio(string path)
        {
            stream = path + "\\Audio";
            directory = Path.GetFullPath(stream);
            Console.WriteLine(directory);

            foreach (string f in Directory.EnumerateFiles(directory))
            {
                try
                {
                    player.SoundLocation = f;
                    player.LoadAsync();
                    Console.WriteLine("Audio load for {0} was successful.", f);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading");
                    Console.WriteLine(ex.Message);
                    
                }
            }
        }
    }
}
