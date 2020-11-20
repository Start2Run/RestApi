using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models
{
    [Table("DbModels")]
    public class DbModel
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Temperature { get; set; }

        public string DateTime { get; set; }
    }
}