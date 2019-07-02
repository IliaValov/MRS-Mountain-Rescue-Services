﻿using System.ComponentModel.DataAnnotations;

namespace MRSMobileServer.ViewModels.Account
{
    public class UserRegisterBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
    }
}