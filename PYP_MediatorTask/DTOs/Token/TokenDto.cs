﻿namespace PYP_MediatorTask.DTOs.Token
{
    public class TokenDto
    {
        public string? AccessToken { get; set; } 
        public DateTime Expiration { get; set; }
    }
}
