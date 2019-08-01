using System;

namespace TeleprompterConsole
{
    //ファイル読み込みの実行状態、ユーザーの入力等を管理する
    internal class TelePrompterConfig
    {
        public int DelayInMilliseconds { get; private set; } = 2;

        public void UpdateDelay(int increment) 
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