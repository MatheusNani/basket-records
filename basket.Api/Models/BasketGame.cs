using System;

namespace basket.Api.Models {
    public class BasketGame {

        public long Id { get; set; }
        public DateTime GameDate { get; set; }
        public int Points { get; set; }
    }
}