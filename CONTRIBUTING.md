# Contributing to MailerSend .NET SDK

Thank you for considering contributing to this third-party MailerSend .NET SDK! We welcome contributions from the community.

> **Note**: This is a community-maintained third-party SDK for the MailerSend API. It is not officially affiliated with or supported by MailerSend.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Setup](#development-setup)
- [How to Contribute](#how-to-contribute)
- [Coding Standards](#coding-standards)
- [Testing Guidelines](#testing-guidelines)
- [Submitting Changes](#submitting-changes)
- [Release Process](#release-process)

## Code of Conduct

This project adheres to a Code of Conduct that all contributors are expected to follow. Please read [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) before contributing.

## Getting Started

1. Fork the repository on GitHub
2. Clone your fork locally
3. Create a new branch for your feature or bugfix
4. Make your changes
5. Test your changes
6. Submit a pull request

## Development Setup

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or later
- A code editor (Visual Studio, VS Code, or Rider recommended)
- Git

### Building the Project

```bash
# Clone your fork
git clone https://github.com/mikeruhl/frenetik.mailerSend.git
cd frenetik.mailerSend

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run tests
dotnet test
```

## How to Contribute

### Reporting Bugs

Before creating a bug report, please check existing issues to avoid duplicates. When creating a bug report, include:

- A clear, descriptive title
- Detailed steps to reproduce the issue
- Expected behavior vs actual behavior
- Your environment (.NET version, OS, etc.)
- Code samples demonstrating the issue
- Any relevant logs or error messages

Use the [bug report template](.github/ISSUE_TEMPLATE/bug_report.md) when creating a new issue.

### Suggesting Features

Feature suggestions are welcome! When suggesting a feature:

- Use a clear, descriptive title
- Provide a detailed description of the proposed feature
- Explain why this feature would be useful
- Include code examples showing how the feature would be used

Use the [feature request template](.github/ISSUE_TEMPLATE/feature_request.md) when suggesting new features.

### Pull Requests

1. **Create an issue first** for significant changes to discuss the approach
2. **Follow the coding standards** outlined below
3. **Write or update tests** for your changes
4. **Update documentation** if you're changing functionality
5. **Keep commits atomic** and write clear commit messages
6. **Ensure all tests pass** before submitting

## Coding Standards

### General Guidelines

- Follow [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful variable and method names
- Keep methods focused and concise
- Add XML documentation comments for all public APIs

### Code Style

- Use 4 spaces for indentation (not tabs)
- Place opening braces on new lines (Allman style)
- Use `var` only when the type is obvious from the right side
- Prefer `async/await` over `Task.ContinueWith`
- Always use `ConfigureAwait(false)` in library code

### Comments

- Use comments sparingly - code should be self-documenting
- Don't add obvious comments
- Comments should explain "why" not "what"
- **Exception**: Always use XML documentation comments for public APIs

Example:
```csharp
/// <summary>
/// Sends an email message through the MailerSend API.
/// </summary>
/// <param name="email">The email message to send.</param>
/// <returns>A response containing the message ID and rate limit information.</returns>
/// <exception cref="MailerSendException">Thrown when the API request fails.</exception>
public async Task<MailerSendResponse> SendAsync(Email email)
{
    // Implementation
}
```

### Architecture Patterns

This SDK follows specific architectural patterns:

- **Dependency Injection**: All services require `IHttpClientFactory` and `MailerSendOptions`
- **Service Pattern**: Each API domain has its own service class
- **Fluent Builders**: Use method chaining for complex object construction (e.g., Email builder)
- **Proper Resource Management**: Implement `IDisposable` where appropriate

### Multi-Targeting

The SDK targets both `net8.0` and `netstandard2.0`. When adding new features:

- Test on both target frameworks
- Use conditional compilation (`#if NETSTANDARD2_0`) when necessary
- Avoid APIs not available in .NET Standard 2.0

## Testing Guidelines

### Writing Tests

- Use xUnit for all tests
- Mock HTTP responses using `HttpMessageHandler`
- Follow the existing test patterns in `MailerSend.Tests`
- Test both success and error scenarios
- Include edge cases and boundary conditions

### Test Structure

```csharp
[Fact]
public async Task MethodName_Scenario_ExpectedResult()
{
    // Arrange
    var mockHandler = new MockHttpMessageHandler(responseJson, HttpStatusCode.OK);
    var service = CreateService(mockHandler);

    // Act
    var result = await service.MethodAsync();

    // Assert
    Assert.NotNull(result);
    Assert.Equal(expectedValue, result.Property);
}
```

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test /p:CollectCoverage=true

# Run specific test class
dotnet test --filter "FullyQualifiedName~EmailServiceTests"
```

### Test Coverage

- Aim for high test coverage on new code
- All public methods should have tests
- Critical paths must have comprehensive tests

## Submitting Changes

### Commit Messages

Write clear, concise commit messages:

```
Add support for SMS personalization

- Implement personalization parameters for SMS messages
- Add unit tests for SMS personalization
- Update documentation with examples
```

### Pull Request Process

1. **Update your branch** with the latest changes from main:
   ```bash
   git fetch upstream
   git rebase upstream/main
   ```

2. **Ensure all tests pass**:
   ```bash
   dotnet test
   ```

3. **Update the README** if you're adding new features

4. **Create a pull request** with:
   - A clear title describing the change
   - Reference to related issues (e.g., "Fixes #123")
   - Description of changes and why they're needed
   - Screenshots for UI changes (if applicable)

5. **Address review feedback** promptly and professionally

### Pull Request Checklist

- [ ] Code follows the project's style guidelines
- [ ] Self-review completed
- [ ] Comments added for complex logic
- [ ] XML documentation added for public APIs
- [ ] Tests added/updated and passing
- [ ] Documentation updated
- [ ] No breaking changes (or clearly documented)

## Release Process

Releases are managed by the maintainers. The process involves:

1. Version bumping in `.csproj` files
2. Updating `CHANGELOG.md`
3. Creating a git tag with the version (e.g., `v1.2.0`)
4. GitHub Actions automatically publishes to NuGet

### Semantic Versioning

This project follows [Semantic Versioning](https://semver.org/):

- **MAJOR**: Breaking changes
- **MINOR**: New features, backward compatible
- **PATCH**: Bug fixes, backward compatible

### Preview Releases

Preview releases use version suffixes (e.g., `v1.2.0-preview`, `v1.2.0-beta`).

## Questions?

If you have questions about contributing:

- Open an issue with the `question` label
- Check the [MailerSend API documentation](https://developers.mailersend.com) for API-specific questions

Thank you for contributing to this community SDK!
