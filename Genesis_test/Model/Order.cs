using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace Genesis_test.Model;

public class Order
{

    #region Variable



    #endregion

    #region Property

    [SwaggerIgnore]
    public int Id { get; set; }
    public int WholesalerId { get; set; }
    public List<OrderLine> Lines { get; set; }

    [SwaggerIgnore]
    public double TotalHt { get; set; }

    #endregion

    #region Constructor

    public Order()
    {
        Lines = new List<OrderLine>();
    }

    #endregion

    #region Method

    #endregion

    #region Inherited

    #endregion

    
}