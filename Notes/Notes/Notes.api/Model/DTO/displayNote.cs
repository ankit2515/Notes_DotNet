﻿namespace Notes.api.Model.DTO
{
    public class displayNote
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
       
        public DateTime DateCreated { get; set; }

    }
}
