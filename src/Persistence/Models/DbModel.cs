using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models
{
    [Table("DbModels")]
    public class DbModel : IEquatable<DbModel>
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Temperature { get; set; }

        public string DateTime { get; set; }

        public bool Equals(DbModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Longitude.Equals(other.Longitude) && Latitude.Equals(other.Latitude) && Temperature.Equals(other.Temperature) && DateTime == other.DateTime;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((DbModel) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Longitude, Latitude, Temperature, DateTime);
        }
    }
}