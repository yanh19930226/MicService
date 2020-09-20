using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Contact.Api.Models.ViewModel
{
    public class TagInputViewModel
    {
        public int ContactId { get; set; }
        public List<string> Tags { get; set; }
    }
}
