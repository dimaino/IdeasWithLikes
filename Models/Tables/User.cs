using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlackBeltTest2.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int id {get; set;}
        public string First_Name {get; set;}
        public string Last_Name {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public User()
        {
            
        }        
    }
}