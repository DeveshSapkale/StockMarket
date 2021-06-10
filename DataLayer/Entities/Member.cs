using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    [DataContract]
    public class Member
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string EmailId { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string LastSearchAll { get; set; }
        [DataMember]
        public string LastSearchHistory { get; set; }
        [DataMember]
        public string LastSearchLive { get; set; }
        [DataMember]
        public bool? IsAdmin { get; set; }

    }
}
