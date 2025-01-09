namespace Web.Xmarket.Utilitario
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class AnularResponse
    {
        public AnularResponse()
        {
            this.Result = new Result();
        }

        public Result Result { get; set; }
    }
}
