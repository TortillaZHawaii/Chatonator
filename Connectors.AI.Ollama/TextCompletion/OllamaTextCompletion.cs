using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel.AI;
using Microsoft.SemanticKernel.AI.TextCompletion;
using Microsoft.SemanticKernel.Orchestration;

namespace Connectors.AI.Ollama.TextCompletion;

/// <summary>
/// Allows semantic kernel to use models hosted using Ollama as AI Service
/// </summary>
public class OllamaTextCompletion : ITextCompletion
{
    public IReadOnlyDictionary<string, string> Attributes => _attributes;

    private readonly Dictionary<string, string> _attributes = new();
    private readonly OllamaApiClient _apiClient;
    private readonly ILogger<OllamaTextCompletion> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="OllamaTextCompletion"/> class.
    /// Using default <see cref="HttpClientHandler"/> implementation.
    /// </summary>
    /// <param name="modelId">Ollama model to use</param>
    /// <param name="baseUrl">Ollama endpoint</param>
    /// <param name="loggerFactory">Logger</param>
    public OllamaTextCompletion(string modelId, string baseUrl, ILoggerFactory? loggerFactory)
    {
        _attributes.Add("model_id", modelId);
        _attributes.Add("base_url", baseUrl);

        _apiClient = new OllamaApiClient(new Uri(baseUrl));
        // this._httpClient = new HttpClient(NonDisposableHttpClientHandler.Instance, disposeHandler: false);

        _logger = loggerFactory is not null ? loggerFactory.CreateLogger<OllamaTextCompletion>() : NullLogger<OllamaTextCompletion>.Instance;
    }

    /// <summary>
    /// Generate response using Ollama api
    /// </summary>
    /// <param name="text">Prompt</param>
    /// <param name="requestSettings">Llama2 Settings can be passed as extension data</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IReadOnlyList<ITextResult>> GetCompletionsAsync(string text, AIRequestSettings? requestSettings = null, CancellationToken cancellationToken = default)
    {
        ConversationContext? co = null;
        var result = await _apiClient.GetCompletion(text, _attributes["model_id"], co);

        return new List<ITextResult> { new OllamaTextResponse(new ModelResult(result.Response)) };
    }

    public IAsyncEnumerable<ITextStreamingResult> GetStreamingCompletionsAsync(string text, AIRequestSettings? requestSettings = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
        // ConversationContext? co = null;
        // GenerateCompletionResponseStream stream = null;
        // var result = _apiClient.StreamCompletion(text, this._attributes["model_id"], co, s => stream = s);
        //
        // await foreach (var item in stream.Done)
        // {
        //     yield return new OllamaTextStreamingResponse(new ModelResult(item));
        // }
    }
}