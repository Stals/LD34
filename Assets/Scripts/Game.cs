using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftCore;


public class Game {

    public struct Settings
    {
        public static readonly float DT_UNIT = 1.0f / 60.0f;
    }

    private static Game instance;
    private Game() {
        //player =s Player.load();
        deckCombiner = DeckCombiner.load();
        player = new Player();
    }

    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Game();
            }
            return instance;
        }
    }

    //Player player;
    //public Player Player { get { return player; } }


    GameManager manager;
    DeckCombiner deckCombiner;
    Player player;

    
    public float SoundVolume
    {
        get
        {
            return SoundPlayer.Volume;
        }
        set
        {
            if (SoundPlayer != null)
                SoundPlayer.Volume = value;
            else
                Debug.LogWarning("SoundPlayer does not exist.");
            
        }
    }
    private SoundPlayingManager soundPlayer;
    public SoundPlayingManager SoundPlayer
    {
        get
        {
            return soundPlayer;
        }

        set
        {
            soundPlayer = value;
        }
    }

    public MusicPlayer musicManager;

    /*
    public float MusicVolume
    {
        get { return player.musicVolume; }
        set
        {
            if (MusicMapPlayer != null)
                MusicMapPlayer.Volume = value;
            else
                Debug.LogWarning("MusicMapPlayer does not exist.");
            if (MusicPlayer != null)
                MusicPlayer.Volume = value;
            else
                Debug.LogWarning("MusicPlayer does not exist.");
            player.musicVolume = value;
        }
    }
    private MusicPlayer musicPlayer;
    public MusicPlayer MusicPlayer
    {
        get
        {
            return musicPlayer;
        }

        set
        {
            musicPlayer = value;
        }
    }
    private MusicPlayer musicMapPlayer;
    public MusicPlayer MusicMapPlayer
    {
        get
        {
            return musicMapPlayer;
        }

        set
        {
            musicMapPlayer = value;
        }
    }*/


    public void init(GameManager _manager)
    {
        manager = _manager;
    }

    public GameManager getManager()
    {
        return manager;
    }

    public DeckCombiner getDeckCombiner()
    {
        return deckCombiner;
    }

    public Player getPlayer()
    {
        return player;
    }


    public int getCurrentLevelID()
	{
        return Application.loadedLevel;// int.Parse(Application.loadedLevelName.Substring(5)) - 1;
	}

}