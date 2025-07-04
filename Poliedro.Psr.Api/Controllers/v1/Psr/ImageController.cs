//using Azure;
//using Azure.AI.FormRecognizer;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
//using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
//using System.Text;
//using System.Text.RegularExpressions;
//namespace Poliedro.Psr.Api.Controllers.v1.Psm;

//[Route("api/v1/[controller]")]
//[ApiController]
//public class ImageController : ControllerBase
//{
//    private readonly FormRecognizerClient _recognizerClient;
//    static readonly string key = "00e8c4211c74497e85e40b30c3402b91";
//    static readonly string endpoint = "https://poliedro-smart-meter.cognitiveservices.azure.com/";
//    public ImageController()
//    {
//        var credential = new AzureKeyCredential("00e8c4211c74497e85e40b30c3402b91");
//        _recognizerClient = new FormRecognizerClient(new Uri("https://poliedro-smart-meter.cognitiveservices.azure.com/"), credential);
//    }

//    [HttpPost]
//    public async Task<IActionResult> UploadAndProcessImage([FromForm] IFormFile file)
//    {
//        if (file == null || file.Length == 0)
//        {
//            return BadRequest("No file selected.");
//        }

//        try
//        {
//            using MemoryStream memoryStream = new();
//            await file.CopyToAsync(memoryStream);
//            memoryStream.Position = 0;

//            ComputerVisionClient client = Authenticate(endpoint, key);
//            await ReadFileStream(client, memoryStream);

//            return Ok();
//        }
//        catch (RequestFailedException ex)
//        {
//            return StatusCode(500, $"Error al procesar la imagen: {ex.Message}");
//        }
//        catch (System.Exception ex)
//        {
//            return StatusCode(500, $"Error inesperado: {ex.Message}");
//        }
//    }

//    public static ComputerVisionClient Authenticate(string endpoint, string key)
//    {
//        ComputerVisionClient client =
//          new(new ApiKeyServiceClientCredentials(key))
//          { Endpoint = endpoint };
//        return client;
//    }
//    public static async Task ReadFileStream(ComputerVisionClient client, Stream imageStream)
//    {
//        var textHeaders = await client.ReadInStreamAsync(imageStream);
//        string operationLocation = textHeaders.OperationLocation;
//        string operationId = operationLocation.Split('/').Last();
//        ReadOperationResult results;
//        Console.WriteLine($"Extracting text from image stream...");
//        Console.WriteLine();
//        do
//        {
//            results = await client.GetReadResultAsync(Guid.Parse(operationId));
//        }
//        while ((results.Status == OperationStatusCodes.Running ||
//            results.Status == OperationStatusCodes.NotStarted));

//        Console.WriteLine();
//        var textUrlFileResults = results.AnalyzeResult.ReadResults;
//        if (textUrlFileResults.Count > 0)
//        {
//            foreach (ReadResult page in textUrlFileResults)
//            {
//                foreach (Line line in page.Lines)
//                {
//                    string textWithOutSpace = DeleteSpace(line.Text);
//                    string onlyNumber = GetNumber(textWithOutSpace);
//                    Console.WriteLine(onlyNumber);
//                }
//            }
//        }

//        Console.WriteLine();
//    }
//    private static string GetNumber(string cadena)
//    {
//        string patron = @"\b\d{5}\b";
//        MatchCollection coincidencias = Regex.Matches(cadena, patron);
//        StringBuilder numeros = new();
//        foreach (Match match in coincidencias)
//        {
//            numeros.Append(match.Value);
//        }
//        return numeros.ToString();
//    }
//    private static string DeleteSpace(string cadena)
//    {
//        return cadena.Replace(" ", "");
//    }

//}
