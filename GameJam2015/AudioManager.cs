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

        /// <summary>
        /// Instantiates the AudioManager that plays the background music.
        /// </summary>
        /// <param name="path">Content.RootDirectory</param>
        public AudioManager(string path)
        {
            player = new SoundPlayer();
            
            stream = path + "\\Audio";
            directory = Path.GetFullPath(stream);
        }

        /// <summary>
        /// Specifies which audio file to play, in turn stopping the previous and plays the new.
        /// </summary>
        /// <param name="s">Piece of the file name</param>
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

        /// <summary>
        /// Loads all audio files to be used during the course of the game.
        /// </summary>
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
