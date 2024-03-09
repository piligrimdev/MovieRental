namespace WebApplication1.DTOs
{
    public class CustomerDTO 
    {
        public int Id { get; set; }
        private string name;
        public string Name { get { return name; } set { name = value; } }
        
        public MemberShipTypeDTO? MemberShipType { get; set; }   
        public byte MemberShipTypeId { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
