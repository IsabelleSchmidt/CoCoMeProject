using System;

namespace EnterpriseServer.Models
{
    public class ReportModel
    {
        public int EnterpriseId { get; set; }

        public int ProductId { get; set; }

        public string Productname { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DeliverDate { get; set; }

        public string status { get; set; }

    }
}
