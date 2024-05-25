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
        return Ok(model);
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
  

/*

{
    [Route("api/[controller]")]
    [ApiController]
    public class BurmeseRecipesController : ControllerBase
    {
        public async Task<BurmeseRecipeList> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("BurmeseRecipes.json");
            var arrayJson = jsonStr.ToArray();
            var model = JsonConvert.DeserializeObject<BurmeseRecipeList>(jsonStr);
            return model;
        }
        [HttpGet()]
        public async Task<IActionResult> BurmeseRecipeMenu()
        {
            var model = await GetDataAsync();
            return Ok(model.BurmeseRecipe);
        }

        //[HttpGet("{guid}")]
        //public async Task<IActionResult> Detail(string guid)
        //{
        //    var model = await GetDataAsync();
        //    return Ok(model.BurmeseRecipe.FirstOrDefault(x => x.Guid == guid));
        //}

        [HttpGet("{UserEngType}")]
        public async Task<IActionResult> GetBurmeseRecipeByType(string UserEngType)
        {
            var model = await GetDataAsync();
            var item = model.UserTypes.FirstOrDefault(x => x.UserEngType == UserEngType);
            if (item is null) return NotFound();

            var UserCode = item.UserCode;
            var lst = model.BurmeseRecipe.Where(x => x.UserType == UserCode);
            return Ok(lst);
        }

        public class BurmeseRecipeList
        {
            public BurmeseRecipe[] BurmeseRecipe { get; set; }
            public Usertype[] UserTypes { get; set; }
        }

        public class BurmeseRecipe
        {
            public string Guid { get; set; }
            public string Name { get; set; }
            public string Ingredients { get; set; }
            public string CookingInstructions { get; set; }
            public string UserType { get; set; }
        }

        public class Usertype
        {
            public int UserId { get; set; }
            public string UserCode { get; set; }
            public string UserMMType { get; set; }
            public string UserEngType { get; set; }
        }


    }
} */