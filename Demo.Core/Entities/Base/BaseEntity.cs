using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Demo.Core.Entities.Base
{
    public class BaseEntity : Entity
    {
        [ForeignKey("User")]
        public int CreatedBy { get; set; }
    }
}
