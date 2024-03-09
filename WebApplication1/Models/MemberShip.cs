using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class MemberShip
    {

        public int Id { get; set;  }

        public string Name { get; set; }

        public byte MonthDuration { get; set; }

        public byte DiscountInPercents { get; set; }

        public short Price { get; set; }
    }
}