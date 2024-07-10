using Swashbuckle.AspNetCore.Annotations;

namespace Genesis_test.Model;

public class Stock
{
    #region Variable

    #endregion

    #region Property

    [SwaggerIgnore]
    public int Id { get; set; }
    public int WholesalerId { get; set; }
    public int BeerId { get; set; }
    public int Nb { get; set; }

    #endregion

    #region Constructor

    #endregion

    #region Method

    #endregion

    #region Inherited

    #endregion


}