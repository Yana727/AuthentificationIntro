using System;
using AuthentificationIntro.Models;

namespace AuthentificationIntro.Models
{
    public class ToDoModel
    {
        public string UserId { get; set; } //had to add for authentification
        public  ApplicationUser User { get; set; } //had to add for the authentification
        public bool Complete { get; set; } = false;
        public DateTime Time { get; set; } = DateTime.Now;
        public int ID {get; set; }
        public string TaskName {get; set;}

        public void Finished(){
            Complete = true; 
            Time = DateTime.Now; 
        }
    }
}