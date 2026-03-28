namespace Application.Dtos.Member
{
    public class CreateMemberDto
    {
      

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string MembershipType { get; set; } // e.g., Student , Public ,Researcher ,GovernmentEmployee

        public string MemberCode { get; set; } // Unique code for each member
    }
}
