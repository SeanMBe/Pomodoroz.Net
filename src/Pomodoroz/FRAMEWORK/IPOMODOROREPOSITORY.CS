using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinnipegTomatoes.Models
{
    public interface IPomodoroRepository
    {
        IEnumerable<Pomodoro> GetPomodoros(string username);
        Pomodoro GetPomodoro(int pomodoroId, string username);
        void AddPomodoro(Pomodoro pomodoro);
        void Save();
    }
}
