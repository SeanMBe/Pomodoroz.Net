using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinnipegTomatoes.Models
{
    public class PomodoroRepository : IPomodoroRepository
    {
        PomodoroContext _context;

        public PomodoroRepository()
        {
            _context = new PomodoroContext();
        }

        public IEnumerable<Pomodoro> GetPomodoros(string username)
        {
            throw new NotImplementedException();
        }
        
        public void AddPomodoro(Pomodoro pomodoro)
        {
            IEnumerable<Pomodoro> openPomodoros = _context.Pomodoroes.Where(p => p.Username == pomodoro.Username && 
                (p.CompletedAt == null && p.ResetAt == null ));
            foreach (var openPomodoro in openPomodoros)
            {
                openPomodoro.ResetAt = DateTime.Now;
            }
                
            _context.Pomodoroes.Add(pomodoro);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        
        public Pomodoro GetPomodoro(int pomodoroId, string username)
        {
            Pomodoro result = _context.Pomodoroes.Where(p => p.PomodoroId == pomodoroId && p.Username == username).FirstOrDefault();
            return result;
        }
    }
}