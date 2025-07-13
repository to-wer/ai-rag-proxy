# AiRagProxy

AiRagProxy is an OpenAI API-compatible ASP.NET Core Web API that implements Retrieval-Augmented Generation (RAG). The project is designed to be provider-agnostic and extensible, allowing integration with various language models and vector search backends. In the future, it will support advanced features such as Semantic Kernel function calling.

## Features

- OpenAI API compatible endpoints (e.g., `/v1/chat/completions`)
- Retrieval-Augmented Generation (RAG) to enhance responses with custom knowledge
- Modular architecture with provider abstraction (OpenAI, Ollama, Azure, etc.)
- Easy extension for embedding services and vector stores
- Planned integration with Microsoft Semantic Kernel for function calling and workflow orchestration

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- API key(s) for your chosen language model provider (e.g., OpenAI)

### Running the API

1. Clone the repository:
```bash
   git clone https://github.com/to-wer/ai-rag-proxy.git
   cd ai-rag-proxy
```

2. Configure your API keys in `appsettings.json` or environment variables:

```json
   {
     "OpenAI": {
       "ApiKey": "your-openai-api-key"
     }
   }
```

3. Run the project:

```bash
   dotnet run --project src/AiRagProxy.Api/AiRagProxy.Api.csproj
   ```

4. The API will be available at `https://localhost:5001/v1/chat/completions`. Test the API with:
```bash
   curl -X POST "https://localhost:5001/v1/chat/completions" -H "Content-Type: application/json" -d '{
     "model": "gpt-4",
     "messages": [{"role": "user", "content": "What is RAG?"}]
   }'
```


## Project Structure

* `src/AiRagProxy.Api`: The main Web API project exposing OpenAI-compatible endpoints
* `src/AiRagProxy.Core`: (planned) Core services and interfaces for RAG and providers
* `src/AiRagProxy.Providers`: (planned) Implementation of different model providers
* `tests/`: Unit and integration tests

## Roadmap

* [ ] Basic RAG implementation with embedding and vector retrieval
* [ ] Support for multiple language model providers
* [ ] Semantic Kernel integration for function calling and plugins
* [ ] Docker support and deployment guides
* [ ] Enhanced security and authentication features

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests.

## License

This project is licensed under the MIT License.
