# Mini Company Knowledge API

## Overview

Company Knowledge API is a lightweight ASP.NET Core Web API that allows users to ask questions about internal company information.

The system retrieves answers from a set of documents stored in the `docs/` directory.

This project is intentionally designed as a simple MVP to demonstrate:

* Knowledge retrieval from company documents
* Clean project structure
* Agent-friendly documentation
* AI-assisted software development workflow

---

## Problem Statement

Employees frequently need answers to questions such as:

* What are the company working hours?
* How many annual leave days are available?
* What products does the company provide?

Instead of searching multiple documents manually, users can ask questions through a single API endpoint.

---

## MVP Scope

Current MVP supports:

* Document-based knowledge storage
* Question submission through API
* Basic keyword matching
* Source document identification
* Agent-oriented project documentation

Not included in MVP:

* Semantic search
* Embeddings
* Vector databases
* LLM-generated answers
* Authentication

---

## Repository Structure

```text
CompanyKnowledgeApi/

docs/
    faq.txt
    leave-policy.txt
    product-info.txt

src/
    Models/
    Services/
    Program.cs

agentic-brain/
    PROJECT_BRIEF.md
    AGENT_CONTEXT.md
    MEMORY.md
    TASKS.md
    EVALS.md

README.md
```

---

## Architecture

```text
User Question
      ↓
KnowledgeService
      ↓
Load Documents
      ↓
Keyword Search
      ↓
Best Match
      ↓
API Response
```

---

## API Endpoint

### Ask Question

POST /ask

Request:

```json
{
  "question": "What is the main company product?"
}
```

Response:

```json
{
  "answer": "SmartCRM is the company's main product.",
  "source": "product-info.txt"
}
```

---

## Running the Project

### Prerequisites

* .NET 9 SDK

### Run

```bash
dotnet restore
dotnet run
```

Default endpoint:

```text
http://localhost:5000
```

---

## Testing

Evaluation scenarios are stored in:

```text
agentic-brain/EVALS.md
```

Examples:

* Working hours
* Annual leave policy
* Remote work policy
* Product information
* Approval requirements

Future versions should include automated xUnit tests.

---

## AI-Assisted Development

AI tools may be used for:

* Initial project scaffolding
* Code generation
* Documentation drafting
* Test case generation
* Code review support

Human review is required for:

* Architecture decisions
* Business rules
* Quality validation
* Security considerations

---

## Known Limitations

Current search implementation uses simple keyword matching.

Potential issues:

* Synonym handling
* Ranking quality
* Multi-language support
* Large document collections

---

## Future Roadmap

### Phase 1

* Add Swagger
* Add xUnit tests
* Improve matching algorithm

### Phase 2

* TF-IDF search
* Document chunking
* Better ranking

### Phase 3

* Embeddings
* Vector search
* Semantic retrieval

### Phase 4

* Retrieval-Augmented Generation (RAG)
* LLM integration
* Source citations

---

## Agent Documentation

Project-specific agent knowledge is stored in:

* PROJECT_BRIEF.md
* AGENT_CONTEXT.md
* MEMORY.md
* TASKS.md
* EVALS.md

These files are intended to help future developers and AI coding agents understand the project state and continue development consistently.

---

## Success Criteria

The project is considered successful if:

1. Questions can be answered from documents.
2. Source documents are identified.
3. Project structure is understandable.
4. Documentation enables future continuation.
5. Evaluation scenarios pass consistently.
