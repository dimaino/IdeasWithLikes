using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoginAndRegisterFinal.Models
{
    public class Message : BaseEntity
    {
        [Key]
        public int id {get; set;}
        public string Content {get; set;}
        public Message()
        {
            
        }        
    }
}