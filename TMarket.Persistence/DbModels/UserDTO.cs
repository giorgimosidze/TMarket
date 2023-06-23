using System;
using System.Collections.Generic;
using TMarket.Persistence.DbModels.Interfaces;

namespace TMarket.Persistence.DbModels
{
    public class UserDTO : IDbEntity
    {
        public UserDTO()
        {
            Orders = new HashSet<OrderDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ICollection<OrderDTO> Orders { get; set; }
    }
}
