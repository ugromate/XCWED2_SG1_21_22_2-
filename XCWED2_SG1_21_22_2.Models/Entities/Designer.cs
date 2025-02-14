﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XCWED2_SG1_21_22_2.Models.Entities
{
    [Table("Designers")]
    public class Designer
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Nationality { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<BoardGame> BoardGames { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nNationality: {Nationality}\n";
        }

    }
}
