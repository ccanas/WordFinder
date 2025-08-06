# WordFinder API

A .NET 8 Web API implementation for the **Word Finder coding challenge**.

## 📌 Objective
Given:
- A character **matrix** (maximum size 64x64)
- A **large stream of words**

The service:
- Searches for the words in the matrix.
- Words can appear horizontally (left to right) or vertically (top to bottom).
- Returns **the top 10 most repeated words from the word stream that are found in the matrix**.
- If no words are found, returns an empty set.

---

## ✅ Features Implemented
- Clean architecture with **Controller → MediatR → Service** layers.
- **FluentValidation** for request validation:
  - Matrix and word stream must not be null or empty.
  - All matrix rows must have the same length.
  - No empty or whitespace rows/words.
  - **Matrix size constraint**: max 64 rows and 64 columns.
- **Dependency Injection** for `IWordFinderService`.
- High performance optimizations:
  - Pre-calculated frequencies for word stream.
  - Avoid repeated scans of matrix for the same word.
- **Unit tests** for:
  - Service logic (`WordFinderService`).
  - Validators (`GetFoundWordsRequestValidator`).
  - MediatR handler.
- **Integration tests** using `Microsoft.AspNetCore.Mvc.Testing` for full pipeline validation.

---

### `GET /api/WordFinder`
**Query Parameters:**
- `matrix` → List of strings representing rows of the matrix.
- `wordstream` → List of words to search for.

**Example Request:**
POST /api/WordFinder
```json
{
  "matrix": [
    "abcdc",
    "fgwio",
    "chill"
  ],
  "wordstream": [
    "chill",
    "hot"
  ]
}
```
**Example Response:**
```json
[
  "abc",
  "efg"
]
```

---

## ✅ Constraints
- Matrix must not exceed 64 rows and 64 columns.
- All rows must be equal length.

---
## ✅ Tech Stack
- .NET 8
- ASP.NET Core Web API
- MediatR for request/response handling.
- FluentValidation for validation.
- xUnit for testing.
- Microsoft.AspNetCore.Mvc.Testing for integration tests.

---
## ✅ How to Run Locally
- Clone the repository:
  ```
  git clone https://github.com/your-username/WordFinder.git
  cd WordFinder
  ```

- Restore dependencies:
  ```
  dotnet restore
  ```

- Run the API:
  ```
  dotnet run --project WordFinder
  ```

- Open Swagger UI:
  ```
  https://localhost:5001/swagger
  ```
  
---
## ✅ Project Structure
 ```
  WordFinder/
│
├── Controllers/
│   └── WordFinderController.cs
│
├── Features/
│   ├── GetFoundWordsRequest.cs
│   └── GetFoundWordsHandler.cs
│
├── Service/
│   ├── IWordFinderService.cs
│   └── WordFinderService.cs
│
└── Program.cs
  ```
  
---
