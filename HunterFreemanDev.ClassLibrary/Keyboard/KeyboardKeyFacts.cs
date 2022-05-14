using HunterFreemanDev.ClassLibrary.KeyDown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Keyboard;

public static class KeyboardKeyFacts
{
    public static bool IsMetaKey(KeyDownEventRecord keyDownEventRecord)
    {
        if (keyDownEventRecord.Key.Length > 1)
            return true;

        return false;

        // TODO: Is a switch needed?
        // switch (onKeyDownEventArgs.Code)
        // {
        // 	default:
        // 		return false;
        // }
    }

    public static bool IsWhitespaceKey(KeyDownEventRecord keyDownEventRecord)
    {
        switch (keyDownEventRecord.Code)
        {
            case "\t":
            case WhitespaceKeys.Tab:
            case "\n":
            case WhitespaceKeys.Enter:
            case " ":
            case WhitespaceKeys.Space:
                return true;
            default:
                return false;
        }
    }

    public static class MetaKeys
    {
        public const string Backspace = "Backspace";
        public const string Escape = "Escape";
        public const string Delete = "Delete";
        public const string F10 = "F10";
    }

    public static class WhitespaceKeys
    {
        public const string Tab = "Tab";
        public const string Enter = "Enter";
        public const string Space = "Space";
    }

    public static class MovementKeys
    {
        public const string ArrowLeft = "ArrowLeft";
        public const string ArrowDown = "ArrowDown";
        public const string ArrowUp = "ArrowUp";
        public const string ArrowRight = "ArrowRight";
        public const string Home = "Home";
        public const string End = "End";
    }

    public static class AlternateMovementKeys
    {
        public const string ArrowLeft = "h";
        public const string ArrowDown = "j";
        public const string ArrowUp = "k";
        public const string ArrowRight = "l";
    }

    public static bool IsMovementKey(KeyDownEventRecord keyDownEventRecord)
    {
        switch (keyDownEventRecord.Key)
        {
            case MovementKeys.ArrowLeft:
            case MovementKeys.ArrowDown:
            case MovementKeys.ArrowUp:
            case MovementKeys.ArrowRight:
            case MovementKeys.Home:
            case MovementKeys.End:
                return true;
            default:
                return false;
        }
    }
}
