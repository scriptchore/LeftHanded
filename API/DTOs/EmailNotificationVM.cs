﻿namespace API.DTOs
{
    public class EmailNotificationVM
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
