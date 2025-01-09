using QueryContracts.Xmarket.Editorial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Models.Nosotros
{
    public class NosotrosModel
    {
        public IEnumerable<EditorialDTO> editoriales { get; set; }
    }
}