using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlackBeltTest2.Models
{
    public class Idea : BaseEntity
    {
        public int IdeaId {get; set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public string Content {get;set;}
        public List<Like> IdeasLikes {get;set;} 
        public Idea()
        {
            IdeasLikes = new List<Like>();
        }        
    }
}