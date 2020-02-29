using System;
using System.ComponentModel.DataAnnotations;

namespace basket.Api.Dtos {
    public class BasketGameDto {

        [Required]
        public DateTime GameDate { get; set; }

        [Required]
        public string Points { get; set; }
    }
}