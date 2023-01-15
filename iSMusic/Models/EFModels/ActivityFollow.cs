namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ActivityFollow
    {
        public int id { get; set; }

        public int activityId { get; set; }

        public int memberId { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual Member Member { get; set; }
    }
}
