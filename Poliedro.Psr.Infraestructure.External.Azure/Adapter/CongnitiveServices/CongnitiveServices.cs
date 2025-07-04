using Azure;
using Azure.AI.FormRecognizer;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Configuration;
using Poliedro.Psr.Domain.Entites;
using Poliedro.Psr.Domain.Ports;
using System.Text;
using System.Text.RegularExpressions;

namespace Poliedro.Psr.Infraestructure.External.Azure.Adapter.CongnitiveServices;

public class CongnitiveServices : ICognitiveService
{
    private readonly FormRecognizerClient _recognizerClient;
    private readonly ComputerVisionClient _computerVisionClient;
    private readonly IConfiguration _configuration;
    private readonly string _key;
    private readonly string _endpoint;
    private readonly string _patter;
    public CongnitiveServices(IConfiguration configuration)
    {
        _configuration = configuration;
        _key = _configuration["CognitiveServiceSettings:key"]!;
        _endpoint = _configuration["CognitiveServiceSettings:endpoint"]!;
        _patter = _configuration["CognitiveServiceSettings:patter"]!;
        if (string.IsNullOrEmpty(_key) || string.IsNullOrEmpty(_endpoint))
        {
            throw new ArgumentNullException("not empty");
        }

        var credential = new AzureKeyCredential(_key);
        _recognizerClient = new FormRecognizerClient(new Uri(_endpoint), credential);
        _computerVisionClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_key)){Endpoint = _endpoint};
    }

    public async Task<UserReaderEntity> ProcessImageAsync(Stream imageStream)
    {
            using var memoryStream = new MemoryStream();
        await imageStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return await ReadFileStreamAsync(_computerVisionClient, memoryStream);
    }

    private async Task<UserReaderEntity> ReadFileStreamAsync(ComputerVisionClient client, Stream imageStream)
    {
        var textHeaders = await client.ReadInStreamAsync(imageStream);
        string onlyNumber = string.Empty;
        string operationLocation = textHeaders.OperationLocation;
        string operationId = operationLocation.Split('/').Last();
        ReadOperationResult results;

        do
        {
            results = await client.GetReadResultAsync(Guid.Parse(operationId));
        }
        while (results.Status == OperationStatusCodes.Running || results.Status == OperationStatusCodes.NotStarted);

        var textUrlFileResults = results.AnalyzeResult.ReadResults;
        if (textUrlFileResults.Count > 0)
        {
            foreach (ReadResult page in textUrlFileResults)
            {
                foreach (Line line in page.Lines)
                {
                    string textWithOutSpace = DeleteSpace(line.Text);
                    onlyNumber = GetNumber(textWithOutSpace);
                    Console.WriteLine(onlyNumber);
                }
            }
        }
        return new UserReaderEntity(Id: Guid.NewGuid(), UserId: Guid.NewGuid(), DataReader: int.Parse(onlyNumber), DateTime: DateTime.Now);
    }
    private string GetNumber(string cadena)
    {
        string patron = @_patter;
        MatchCollection coincidencias = Regex.Matches(cadena, patron);
        StringBuilder numeros = new();
        foreach (Match match in coincidencias)
        {
            numeros.Append(match.Value);
        }
        return numeros.ToString();
    }
    private static string DeleteSpace(string cadena)
    {
        return cadena.Replace(" ", "");
    }
}

