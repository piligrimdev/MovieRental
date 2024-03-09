using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        private string name;

        [Required]
        [MaxLength(255)]
        public string Name { get { return name; }  set { name = value; } }

        public MemberShip? MemberShipType { get; set; }

        //Храним Id отдельно от MemberShip, чтобы можно было занулить сам membershipType и 
        // присовоить его уже после проверки формы
        [Required]
        [Display(Name = "Membership type")]
        public byte MemberShipTypeId { get; set; }

        // Мб бизнес-логика типа нельзя рождение меньше 1900 поставить
        [Required]
        [AtLeast18IfAMember]
        public DateTime BirthDay { get; set; }

    }
}
