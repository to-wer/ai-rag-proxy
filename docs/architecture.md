# AiRagProxy – Architecture Overview

## Overview

AiRagProxy is an OpenAI API-compatible ASP.NET Core Web API that integrates Retrieval-Augmented Generation (RAG). The system is designed to be model- and provider-agnostic, and it will support multiple authentication methods, monitoring, and advanced features such as Semantic Kernel function calling.

---

## 🔧 Tech Stack

- ASP.NET Core Web API
- .NET 9
- Entity Framework Core (planned)
- MongoDB (optional)
- Semantic Kernel (planned)
- Tabler.io (planned UI)
- OpenTelemetry (planned for monitoring)

---

## 📐 High-Level Architecture

```

Client (Chat UI, API Consumers)
│
▼
\[Authentication Layer (OIDC, Entra ID, Keycloak)]
│
▼
AiRagProxy.Api (REST API)
│
├── /v1/chat/completions
│       └── Request Pipeline:
│             1. Validate + Auth
│             2. Embedding (if RAG enabled)
│             3. Vector Retrieval
│             4. Prompt Augmentation
│             5. Model Completion
│
└── /v1/embeddings (optional)

```
    ▼
```

Core Services
├── IModelProvider
├── IEmbeddingService
├── IIndexService
└── IRetrievalService

```
    ▼
```

Model Providers
├── OpenAIProvider
├── AzureOpenAIProvider
├── OllamaProvider
└── CohereProvider (planned)

```
    ▼
```

Vector Store / Index
├── In-Memory (dev)
├── EF Core (PostgreSQL, planned)
└── MongoDB (optional)

````

---

## 🔌 Model Provider Abstraction

Each language model is accessed through a common interface:

```csharp
public interface IModelProvider
{
    Task<ChatCompletionResponse> GetChatCompletionAsync(ChatCompletionRequest request);
    Task<EmbeddingResponse> GetEmbeddingAsync(EmbeddingRequest request);
}
````

Providers implement the interface and can be selected via `appsettings.json`:

```json
"ModelProvider": {
  "Active": "OpenAI",
  "Providers": {
    "OpenAI": {
      "ApiKey": "xxx",
      "BaseUrl": "https://api.openai.com/v1/"
    },
    "Ollama": {
      "BaseUrl": "http://localhost:11434",
      "Model": "mistral"
    }
  }
}
```

---

## 📄 Configuration Example

### `appsettings.json`

```json
{
  "OpenAI": {
    "ApiKey": "..."
  },
  "Authentication": {
    "Mode": "Entra",
    "Entra": {
      "TenantId": "...",
      "ClientId": "...",
      "Audience": "api://..."
    }
  }
}
```

---

## 🔒 Authentication and Authorization

Supported Modes:

* **Azure Entra ID**
  OIDC-based JWT auth with API scopes and App Registration
* **Keycloak**
  Full OIDC support for self-hosted or company SSO
* **Local Auth**
  Simplified dev-mode authentication

The authentication layer is pluggable using `Microsoft.AspNetCore.Authentication.JwtBearer`.

---

## 📂 Project Structure (planned)

```
AiRagProxy.sln
│
├── src/
│   ├── AiRagProxy.Api/         # Main ASP.NET Core Web API
│   ├── AiRagProxy.Core/        # Interfaces and shared logic
│   ├── AiRagProxy.Providers/   # Model provider implementations
│   ├── AiRagProxy.Index/       # Embedding + vector index handling
│   └── AiRagProxy.Semantic/    # Integration with Semantic Kernel (future)
│
├── tests/
│   └── AiRagProxy.Api.Tests/
│
└── docs/
    └── architecture.md
```

---

## 📊 Monitoring (Planned)

* Token usage per user and provider
* Request statistics
* OpenTelemetry integration

---

## 📅 Roadmap Summary

| Component                   | Status        |
| --------------------------- | ------------- |
| OpenAI-compatible endpoints | ✅ Ready       |
| Provider abstraction        | ✅ Ready       |
| RAG core services           | ⏳ In progress |
| Multi-provider support      | ⏳ In progress |
| Semantic Kernel integration | ⏳ Planned     |
| Authentication (OIDC)       | ⏳ In progress |
| UI (Tabler.io)              | ⏳ Planned     |
| Monitoring + telemetry      | ⏳ Planned     |

---

## 🧠 Retrieval-Augmented Generation (RAG)

1. **EmbeddingService**: Converts documents or queries into vector embeddings
2. **IndexService**: Stores/retrieves document chunks with metadata
3. **RetrievalService**: Finds relevant chunks based on query
4. **PromptBuilder**: Constructs a context-enhanced prompt for completion

---

## 💡 Extensibility Goals

* Add new providers with minimal effort
* Allow configuration-based switching (no redeploys)
* Enable fine-grained security (role-based access, scopes)
* Build multiple UI frontends (admin/user separation)

---

*This document will be extended as the project evolves.*