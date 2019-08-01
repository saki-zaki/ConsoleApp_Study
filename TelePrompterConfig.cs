﻿using System;

namespace TeleprompterConsole
{
    internal class TelePrompterConfig
    {
        public int DelayInMilliseconds { get; private set; } = 2;

        public void UpdateDelay(int increment) // negative to speed up
        {
            var newDelay = Math.Min(DelayInMilliseconds + increment, 1000);
            newDelay = Math.Max(newDelay, 2);
            DelayInMilliseconds = newDelay;
        }

        public bool Done { get; private set; }

        public void SetDone()
        {
            Done = true;
        }
    }
}