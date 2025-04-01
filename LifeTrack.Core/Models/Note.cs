using System;
using System.ComponentModel.DataAnnotations;

namespace LifeTrack.Core.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsSynced { get; set; }

        public int? FolderId { get; set; }
        public virtual NoteFolder Folder { get; set; }

        public string[] Tags { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }

    public class NoteFolder
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public int? ParentFolderId { get; set; }
        public virtual NoteFolder ParentFolder { get; set; }
    }
}