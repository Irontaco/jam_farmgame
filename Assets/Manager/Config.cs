using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

public struct Config
{
    public static readonly Config Default = new Config
    {
        KeyBinds = new Controls
        {
            Up = new Controls.Binding { Primary = KeyCode.W, Secondary = KeyCode.UpArrow },
            Down = new Controls.Binding { Primary = KeyCode.S, Secondary = KeyCode.DownArrow },
            Left = new Controls.Binding { Primary = KeyCode.A, Secondary = KeyCode.LeftArrow },
            Right = new Controls.Binding { Primary = KeyCode.D, Secondary = KeyCode.RightArrow },
            
            Interact = new Controls.Binding { Primary = KeyCode.Space, Secondary = KeyCode.Return }
        },
        GraphicsSettings = new Graphics
        {
            Res = new Graphics.Resolution { Width = 1600, Height = 900 },
            WindowMode = Graphics.Mode.Windowed
        },
        AudioSettings = new Audio
        {
            MusicVolume = 0.5f,
            SoundVolume = 0.5f
        }
    };

    public struct Controls
    {
        public struct Binding
        {
            public KeyCode Primary;
            public KeyCode Secondary;

            public bool Held => Input.GetKey(Primary) || Input.GetKey(Secondary);
            public bool Hit => Input.GetKeyDown(Primary) || Input.GetKeyDown(Secondary);
            public bool Lifted => Input.GetKeyUp(Primary) || Input.GetKeyUp(Secondary);
        }
        
        #warning TODO: determine required inputs
        public Binding Up;
        public Binding Down;
        public Binding Left;
        public Binding Right;

        public Binding Interact;
    }

    public struct Graphics
    {
        public struct Resolution
        {
            public int Width;
            public int Height;
        }

        public enum Mode
        {
            Fullscreen,
            Windowed
        }

        public Resolution Res;
        public Mode WindowMode;
    }

    public struct Audio
    {
        public float MusicVolume;
        public float SoundVolume;
    }

    public Controls KeyBinds;
    public Graphics GraphicsSettings;
    public Audio AudioSettings;
}
