using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using basket.Api.Models;

namespace basket.Api.Data {
    public interface IBasketGameRepository {
        Task<BasketGame> Add (BasketGame game);
        Task<List<BasketGame>> List ();
        Task<RecordGame> AddRecord (BasketGame game);

        Task<List<RecordGame>> ListRecords();
    }
}