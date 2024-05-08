using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_ef_videogame
{
    [Table("videogames")]
    public class Videogame
    {
        [Column("id")]
        [Key] public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("overview")]
        public string Description { get; set; }

        [Column("release_date")]
        public DateTime Release { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set;}

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set;}

        [Column("software_house_id")]
        public long SoftwareHouseID {  get; set; }

        public SoftwareHouse Softwarehouse { get; set; }
    }

    [Table("software_houses")]
    public class SoftwareHouse
    {
        [Column("id")]
        [Key] public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("tax_id")]
        public string Code { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public List<Videogame> Videogames { get; set; }
    }
}
