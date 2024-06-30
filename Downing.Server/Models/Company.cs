using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Downing.Server.Models;

public partial class Company
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? CompanyName { get; set; }

    [DataType(DataType.DateTime)]
    [Column(TypeName = "datetime2")]
    public DateTime? CreatedDate { get; set; }

    [Required]
    [MaxLength(10)]
    public string? Code { get; set; }


    public decimal? SharePrice { get; set; }
}
