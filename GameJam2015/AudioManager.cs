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
            player = new SoundPlayer();
            
            stream = path + "\\Audio";
            directory = Path.GetFullPath(stream);
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
                    else
                    {
                        Console.WriteLine("Incorrect match.");
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
            foreach (string f in Directory.EnumerateFiles(directory))
            {
                try
                {
                    //player.SoundLocation = f;
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
