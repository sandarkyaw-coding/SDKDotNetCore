using Newtonsoft.Json;
using SDKDotNetCore.RestApiWithNLayer.Features.Myanmar_Proverbs;

namespace SDKDotNetCore.RestApiWithNLayer.Features.BurmeseRecipes;

[Route("api/[controller]")]
[ApiController]
public class BurmeseRecipeController : ControllerBase
{
    private async Task<List<Recipes>> GetDataFromApiAsync()
    {
        var jsonStr = await System.IO.File.ReadAllTextAsync("BurmeseRecipes.json");
        var model = JsonConvert.DeserializeObject<List<Recipes>>(jsonStr);
        return model!;
    }

    [HttpGet("Menu")]
    public async Task<IActionResult> Get()
    {
        var model = await GetDataFromApiAsync();
        List<RecipesName> lst = model.Select(x => new RecipesName
        {
            Guid = x.Guid,
            Name = x.Name
        }).ToList();

        return Ok(lst);
    }

    [HttpGet("Menu/{guid}")]
    public async Task<IActionResult> Get(string guid)
    {
        var model = await GetDataFromApiAsync();
        return Ok(model.FirstOrDefault(x => x.Guid == guid)); 
    } 
}

public class BurmeseRecipes
{
    public Recipes[] recipes { get; set; }
}

public class Recipes
{
    public string Guid { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string CookingInstructions { get; set; }
    public string UserType { get; set; }
}

public class RecipesName
{
    public string Guid { get; set; }
    public string Name { get; set; }
}


