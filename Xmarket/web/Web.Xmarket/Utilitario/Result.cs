namespace Web.Xmarket.Utilitario
{
    using System;
    using System.Collections.Generic;
    public class Result
    {
        public Result()
        {
            this.Success = true;
            this.ErrCode = "";
            this.Message = "";
            this.Mesagges = new List<Result>();
        }
        public bool Success { get; set; }
        public string ErrCode { get; set; }
        public string Message { get; set; }
        public Guid IdError { get; set; }
        public List<Result> Mesagges { get; set; }
    }
}
