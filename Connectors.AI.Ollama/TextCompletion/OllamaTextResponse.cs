using Microsoft.SemanticKernel.AI.TextCompletion;
using Microsoft.SemanticKernel.Orchestration;

namespace Connectors.AI.Ollama.TextCompletion;

internal sealed class OllamaTextResponse(ModelResult modelResult) : ITextResult
{
    public ModelResult ModelResult { get; } = modelResult;

    public Task<string> GetCompletionAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ModelResult.GetResult<string>());
    }
}

internal sealed class OllamaTextStreamingResponse(ModelResult modelResult) : ITextStreamingResult
{
    public ModelResult ModelResult { get; } = modelResult;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async IAsyncEnumerable<string> GetCompletionStreamingAsync(CancellationToken cancellationToken = default)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        yield return ModelResult.GetResult<string>();
    }
}