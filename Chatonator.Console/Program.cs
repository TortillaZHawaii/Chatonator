// See https://aka.ms/new-console-template for more information

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;

Console.WriteLine("Hello, World!");

var builder = new KernelBuilder();
builder.WithOllamaTextCompletionService("mistral:7b", "http://localhost:11434");
var kernel = builder.Build();

var time = kernel.ImportFunctions(new TimePlugin());

var result = await kernel.RunAsync(time["UTCNow"]);

Console.WriteLine(result);
