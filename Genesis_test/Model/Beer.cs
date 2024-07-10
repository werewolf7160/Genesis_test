using Swashbuckle.AspNetCore.Annotations;

namespace Genesis_test.Model;

public class Beer
{

    #region Variable

    #endregion

    #region Property
    [SwaggerIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public double Degree { get; set; }
    public double Price { get; set; }
    public int BrewerId { get; set; }

    #endregion

    #region Constructor

    #endregion

    #region Method

    #endregion

    #region Inherited

    #endregion


}