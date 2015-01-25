using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.IO;
using System.Resources;
using Microsoft.Xna.Framework.Content;

namespace GameJam2015
{
    public class AudioManager
    {
        public SoundPlayer player;
        public string stream, directory;

        public AudioManager(string path)
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
            
            stream = path + "\\Audio";
            directory = Path.GetFullPath(stream);
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

        public void Play(string s)
        {
            foreach (string f in Directory.EnumerateFiles(directory))
            {
                try
                {
                    if (f.ToLower().Contains(s))
                    {
                        Console.WriteLine("Play a new sound?");
                        player.SoundLocation = f;
                        player.Play();
                    }
                    else if (f.ToLower().Contains(s))
                    {
                        player.SoundLocation = f;
                        player.Play();
                    }
                    else
                    {
                        Console.WriteLine("Sound not found");
                    }
                }
                catch
                {
                    Console.WriteLine("Does not play");
                }
            }
        }

        public void LoadAudio()
        {
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
