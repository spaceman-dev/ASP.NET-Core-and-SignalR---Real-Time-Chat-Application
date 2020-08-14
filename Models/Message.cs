using System;

namespace ChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public int ChatID { get; set; }
        public Chat Chat { get; set; }
    }
}