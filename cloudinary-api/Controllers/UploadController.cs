using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace cloudinary_api.Controllers;

[ApiController]
public class UploadController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public UploadController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [Route("api/upload")]
    public ActionResult Upload(IFormFile file)
    {
        var cloudinary = new Cloudinary(new Account(
            cloud: _configuration.GetSection("Cloudinary:CloudName").Value,
            apiKey: _configuration.GetSection("Cloudinary:ApiKey").Value,
            apiSecret: _configuration.GetSection("Cloudinary:ApiSecret").Value
        ));

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream())
        };
        var uploadResult = cloudinary.Upload(uploadParams);

        return Ok(new {uploadResult.Url});
    }
}