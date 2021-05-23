using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//It is used for validation like particular filed is required 
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        //The following Annotaion will make tile Required and add custom error message
        [Required(ErrorMessage = "Please Add title property")]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
