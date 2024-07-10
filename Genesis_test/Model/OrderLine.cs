using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace Genesis_test.Model;

public class OrderLine
{

    #region Variable

    #endregion

    #region Property
    [SwaggerIgnore]
    public int Id { get; set; }
    public int BeerId { get; set; }
    public int Quantity { get; set; }

    [SwaggerIgnore]
    public double TotalHt { get; set; }

    #endregion

    #region Constructor

    #endregion

    #region Method

    #endregion

    #region Inherited

    #endregion

    
}