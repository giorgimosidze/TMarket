using System;

namespace TMarket.Persistence.DbModels.Interfaces
{
    public interface IDbEntity
    {
        bool IsDeleted { get; set; }
        DateTime InsertDate { get; set; }
        DateTime UpdateDate { get; set; }
        DateTime? DeleteDate { get; set; }
    }
}
