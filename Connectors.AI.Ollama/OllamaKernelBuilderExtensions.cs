// ReSharper disable once CheckNamespace

using Connectors.AI.Ollama.TextCompletion;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.AI.TextCompletion;

namespace Microsoft.SemanticKernel;

public static class OllamaKernelBuilderExtensions
{
    public static KernelBuilder WithOllamaTextCompletionService(
        this KernelBuilder builder,
        string modelId,
        string baseUrl,
        string? serviceId = null)
    {
        builder.WithAIService<ITextCompletion>(serviceId, loggerFactory =>
        {
            return new OllamaTextCompletion(modelId, baseUrl, loggerFactory);
        });
        
        return builder;
    }

    public static KernelBuilder WithOllamaTextCompletionService(this KernelBuilder builder, string modelId,
        System.Uri baseUrl, string? serviceId = null)
    {
        builder.WithAIService<ITextCompletion>(serviceId,
            loggerFactory => new OllamaTextCompletion(modelId, baseUrl.AbsoluteUri, loggerFactory)
            );

        return builder;
    }
}
