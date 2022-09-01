﻿using System.ComponentModel.DataAnnotations;

namespace TubimProject.Application.DTOs.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}