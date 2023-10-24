﻿using System.ComponentModel.DataAnnotations;

namespace NGWalksDomain.ModelDTO
{
	public class RegisterRequestDTO
	{
        
        public string UserName { get; set; } = string.Empty;
      
        public string Password { get; set; } = string.Empty;
        public string[] Roles { get; set; } 
    }
}
